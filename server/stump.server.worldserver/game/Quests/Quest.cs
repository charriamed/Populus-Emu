using System.Linq;
using NLog;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Quests;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.DofusProtocol.Messages;
using System.Collections.Generic;

namespace Stump.Server.WorldServer.Game.Quests
{
    public class Quest
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private QuestRecord m_record;
        private bool m_isDirty;

        public Quest(Character owner, QuestRecord record)
        {
            m_record = record;

            Owner = owner;
            Template = QuestManager.Instance.GetQuestTemplate(record.QuestId);

            if (Template == null)
            {
                logger.Error($"Quest id {record.QuestId} doesn't exist");
                return;
            }

            CurrentStep = new QuestStep(this, QuestManager.Instance.GetQuestStep(record.StepId), ObjectivesStatus);
        }

        public Quest(Character owner, QuestStepTemplate step)
        {
            m_record = new QuestRecord()
            {
                Finished = false,
                QuestId = step.QuestId,
                StepId = step.Id,
                OwnerId = owner.Id,
                IsNew = true
            };
            Template = QuestManager.Instance.GetQuestTemplate(step.QuestId);
            Owner = owner;
            CurrentStep = new QuestStep(this, step);
        }

        public Character Owner
        {
            get;
            private set;
        }

        public ushort Id => (ushort)Template.Id;

        public QuestTemplate Template
        {
            get;
            private set;
        }

        public bool Finished
        {
            get
            {
                return m_record.Finished;
            }
            set { m_record.Finished = value; }
        }

        public QuestStep CurrentStep
        {
            get;
            private set;
        }

        private List<QuestObjectiveStatus> ObjectivesStatus
        {
            get { return m_record.Objectives; }
        }

        public void ChangeQuestStep(QuestStepTemplate step)
        {
            CurrentStep?.CancelQuest();
            CurrentStep = new QuestStep(this, step);
            m_record.StepId = step.Id;
        }

        public QuestActiveInformations GetQuestActiveInformations()
        {
            return new QuestActiveDetailedInformations((ushort)Id, (ushort)CurrentStep.Id, CurrentStep.Objectives.Select(x => x.GetQuestObjectiveInformations()).ToArray());
        }

        public void Save(ORM.Database database)
        {
            WorldServer.Instance.IOTaskPool.ExecuteInContext(() =>
            {
                var questPlayerRecord = database.Query<QuestRecord>(string.Format(QuestRecordRelator.FetchByOwner, Owner.Id)).ToList(); // Get tout les records de quête du perso

                if (questPlayerRecord.Any(x => x.QuestId == m_record.QuestId)) // On vérifie si y'a déjà un record contenant la quêteId
                    database.Update(m_record); // Si ui on l'update juste
                else
                    database.Insert(m_record); // Sinon on l'insere

                CurrentStep.Save(database); // Save de l'étape
            });
        }
    }
}