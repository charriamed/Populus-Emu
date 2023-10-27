using System;
using System.Linq;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Quests;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Enums;
using System.Collections.Generic;

namespace Stump.Server.WorldServer.Game.Quests
{
    public class QuestStep
    {
        public event Action<QuestStep> Finished;

        public QuestStep(Quest quest, QuestStepTemplate template)
        {
            Quest = quest;
            Template = template;
            Objectives = template.Objectives.Where(x => !x.IsTriggeredByObjective).Select(x => x.GenerateObjective(Quest.Owner)).ToList();
            Rewards = template.Rewards.Select(x => new QuestReward(x)).ToArray();

            foreach (var objective in Objectives)
            {
                if (!objective.Finished)
                {
                    objective.Completed += OnObjectiveCompleted;
                    objective.EnableObjective();
                }
            }
        }

        public QuestStep(Quest quest, QuestStepTemplate template, List<QuestObjectiveStatus> status)
        {
            Quest = quest;
            Template = template;
            Objectives = status.Where(w => QuestManager.Instance.GetObjectiveTemplate(w.ObjectiveId).StepId == Id).Select(x => QuestManager.Instance.GetObjectiveTemplate(x.ObjectiveId).GenerateObjective(Quest.Owner, x)).ToList();
            Rewards = template.Rewards.Select(x => new QuestReward(x)).ToArray();

            foreach (var objective in Objectives)
            {
                if (!objective.Finished)
                {
                    objective.Completed += OnObjectiveCompleted;
                    objective.EnableObjective();
                }
            }
        }

        public int Id => Template.Id;

        public Quest Quest
        {
            get;
            set;
        }

        public QuestStepTemplate Template
        {
            get;
            set;
        }

        public List<QuestObjective> Objectives
        {
            get;
            set;
        }

        public QuestReward[] Rewards
        {
            get;
            set;
        }

        private void OnObjectiveCompleted(QuestObjective obj)
        {
            if (obj.Template.TriggeredByObjectiveId > 0)
            {
                var objective = QuestManager.Instance.GetObjectiveTemplate(obj.Template.TriggeredByObjectiveId).GenerateObjective(Quest.Owner);
                Objectives.Add(objective);
                objective.Completed += OnObjectiveCompleted;
                objective.EnableObjective();
            }
            Quest.Owner.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 55, Quest.Template.Id);
            if (Objectives.All(x => x.ObjectiveRecord.Status))
            {
                if (Quest.Template.Steps.Last() == Template)
                    FinishQuest();
                else
                    Quest.ChangeQuestStep(Quest.Template.Steps[Quest.Template.Steps.ToList().IndexOf(Template) + 1]);
            }
        }

        public void FinishQuest()
        {
            OnFinished();
            Quest.Finished = true;
            Quest.Owner.QuestCompleted(Quest);
            Quest.Owner.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 56, Quest.Id);
        }

        public void CancelQuest()
        {
            OnFinished();
        }

        private void GiveRewards()
        {
            foreach (var reward in Rewards)
            {
                reward.GiveReward(Quest.Owner);
            }
        }

        public QuestActiveInformations GetQuestActiveInformations()
        {
            return new QuestActiveDetailedInformations((ushort)Quest.Id, (ushort)Id, Objectives.Select(x => x.GetQuestObjectiveInformations()).ToArray());
        }

        protected virtual void OnFinished()
        {
            foreach (var objective in Objectives)
            {
                objective.Completed -= OnObjectiveCompleted;
                objective.DisableObjective();
            }
            Finished?.Invoke(this);
            Quest.Owner.Inventory.AddKamas((ulong)CalculateWonKamas());
            Quest.Owner.AddExperience(CaclulateWonXp());
            GiveRewards();
            Quest.Owner.RefreshStats();
        }

        private double CalculateWonKamas()
        {
            var lvl = Template.KamasScaleWithPlayerLevel ? Quest.Owner.Level : Template.OptimalLevel;
            return Math.Floor((Math.Pow(lvl, 2) + 20 * lvl - 20) * Template.KamasRatio * Template.Duration);
        }

        private double CaclulateWonXp()
        {
            if (Quest.Owner.Level > Template.OptimalLevel)
            {
                var rewardLevel = Math.Min(Quest.Owner.Level, Template.OptimalLevel * 0.7);
                var fixeOptimalLevelExperienceReward = GetFixeExperienceReward(Template.OptimalLevel, Template.Duration, Template.XpRatio);
                var fixeLevelExperienceReward = GetFixeExperienceReward((int)rewardLevel, Template.Duration, Template.XpRatio);
                var reducedOptimalExperienceReward = (1 - 0.7) * fixeOptimalLevelExperienceReward;
                var reducedExperienceReward = 0.7 * fixeLevelExperienceReward;
                var sumExperienceRewards = Math.Floor(reducedOptimalExperienceReward + reducedExperienceReward);
                return Math.Floor(sumExperienceRewards * 2.25);
            }
            else
                return Math.Floor(GetFixeExperienceReward(Quest.Owner.Level, Template.Duration, Template.XpRatio));
        }

        private double GetFixeExperienceReward(int level, double duration, double xpRatio)
        {
            var levelPow = Math.Pow(100 + 2 * level, 2);
            var result = level * levelPow / 20 * duration * xpRatio;
            return result;
        }

        public void Save(ORM.Database database)
        {
            foreach (var objective in Objectives)
                objective.Save(database);
        }
    }
}