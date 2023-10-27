using System;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Quests;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.DofusProtocol.Messages;
using System.Linq;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Handlers.Context.RolePlay;
using Stump.DofusProtocol.Enums;
using Stump.Core.Reflection;

namespace Stump.Server.WorldServer.Game.Quests
{
    public abstract class QuestObjective
    {
        public QuestObjective(Character character, QuestObjectiveTemplate template, bool finished)
        {
            Character = character;
            Template = template;
            Finished = finished;
            ObjectiveRecord = new QuestObjectiveStatus()
            {
                QuestId = StepTemplate.QuestId,
                ObjectiveId = Template.Id,
                Status = Finished,
                OwnerId = character.Id,
                Completion = 0,
                IsNew = true
            };
        }

        public QuestObjective(Character character, QuestObjectiveTemplate template, QuestObjectiveStatus record)
        {
            Character = character;
            Template = template;
            ObjectiveRecord = record;
            Finished = record.Status;
        }

        public QuestObjectiveStatus ObjectiveRecord
        {
            get;
            set;
        }

        public QuestObjectiveTemplate Template
        {
            get;
        }

        public bool Finished
        {
            get;
            private set;
        }

        public Character Character
        {
            get;
            set;
        }


        public QuestStepTemplate StepTemplate
        {
            get { return QuestManager.Instance.GetQuestStep(Template.StepId); }
        }

        public abstract void EnableObjective();
        public abstract void DisableObjective();
        public abstract bool CanSee();
        public abstract int Completion();

        public void CompleteObjective()
        {
            OnCompleted();
        }

        public event Action<QuestObjective> Completed;
        public abstract QuestObjectiveInformations GetQuestObjectiveInformations();

        protected virtual void OnCompleted()
        {
            DisableObjective();
            ObjectiveRecord.Status = true;
            Finished = true;
            Completed?.Invoke(this);
            ContextRoleplayHandler.SendRefreshMapQuestWithout(Character.Client, QuestManager.Instance.GetQuestTemplateWithStepId(Template.StepId).Id);
            ContextRoleplayHandler.SendQuestStepValidatedMessage(Character.Client, (ushort)QuestManager.Instance.GetQuestTemplateWithStepId(Template.StepId).Id, (ushort)Template.StepId);
        }

        public void Save(ORM.Database database)
        {
            WorldServer.Instance.IOTaskPool.ExecuteInContext(() =>
            {
                var objectives = QuestManager.Instance.GetStatusByOwnerId(Character.Id);

                if (objectives.Any(x => x.ObjectiveId == ObjectiveRecord.ObjectiveId && x.QuestId == ObjectiveRecord.QuestId)) // On vérifie si y'a déjà un record contenant la quêteId
                    database.Update(ObjectiveRecord); // Si ui on l'update juste
                else
                    database.Insert(ObjectiveRecord); // Sinon on l'insere
            });
        }
    }
}