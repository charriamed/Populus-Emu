using Stump.Server.WorldServer.Database.Achievements;
using Stump.Server.WorldServer.Game.Achievements.Criterions.Data;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Conditions;
using System;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Achievements.Criterions
{
    public class QuestFinishedCountCriterion : AbstractCriterion<QuestFinishedCountCriterion, DefaultCriterionData>
    {
        // FIELDS
        public const string Identifier = "QQ";
        private ushort? m_maxValue;

        // PROPERTIES
        public int Number
        {
            get
            {
                return base[0][0];
            }
        }
        public override bool IsIncrementable
        {
            get
            {
                return true;
            }
        }
        public override ushort MaxValue
        {
            get
            {
                if (this.m_maxValue == null)
                {
                    this.m_maxValue = new ushort?((ushort)this.Number);

                    switch (base[0].Operator)
                    {
                        case ComparaisonOperatorEnum.EQUALS: break;

                        case ComparaisonOperatorEnum.INFERIOR:
                            throw new Exception();

                        case ComparaisonOperatorEnum.SUPERIOR:
                            this.m_maxValue++;
                            break;

                        default:
                            break;
                    }
                }

                return this.m_maxValue.Value;
            }
        }

        // CONSTRUCTORS
        public QuestFinishedCountCriterion(AchievementObjectiveRecord objective)
            : base(objective)
        { }

        // METHODS
        public override DefaultCriterionData Parse(ComparaisonOperatorEnum @operator, params string[] parameters)
        {
            return new DefaultCriterionData(@operator, parameters);
        }

        public override bool Eval(Character character)
        {
            return this.Number < character.Quests.Where(x => x.Finished).Count();
        }

        public override bool Lower(AbstractCriterion left)
        {
            return this.Number < ((QuestFinishedCountCriterion)left).Number;
        }
        public override bool Greater(AbstractCriterion left)
        {
            return this.Number > ((QuestFinishedCountCriterion)left).Number;
        }

        public override ushort GetPlayerValue(PlayerAchievement player)
        {
            return (ushort)Math.Min(this.MaxValue, player.Owner.Quests.Where(x => x.Finished).Count());
        }
    }
}
