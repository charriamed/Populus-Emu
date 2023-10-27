using Stump.Server.WorldServer.Database.Achievements;
using Stump.Server.WorldServer.Game.Achievements.Criterions.Data;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Conditions;
using Stump.Server.WorldServer.Game.Jobs;
using System;

namespace Stump.Server.WorldServer.Game.Achievements.Criterions
{
    public class CraftCountCriterion : AbstractCriterion<CraftCountCriterion, DefaultCriterionData>
    {
        // FIELDS
        public const string Identifier = "EC";
        private ushort? m_maxValue;

        // PROPERTIES
        public int ItemsCount
        {
            get
            {
                return base[0][1];
            }
        }

        public int ItemId
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
                    this.m_maxValue = new ushort?((ushort)this.ItemsCount);

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
        public CraftCountCriterion(AchievementObjectiveRecord objective)
            : base(objective)
        { }

        // METHODS
        public override DefaultCriterionData Parse(ComparaisonOperatorEnum @operator, params string[] parameters)
        {
            return new DefaultCriterionData(@operator, parameters);
        }

        public override bool Eval(Character character)
        {
            return this.ItemsCount < JobManager.Instance.GetAmountOfCraft(character.Id, ItemId);
        }

        public override bool Lower(AbstractCriterion left)
        {
            return this.ItemsCount < ((CraftCountCriterion)left).ItemsCount;
        }
        public override bool Greater(AbstractCriterion left)
        {
            return this.ItemsCount > ((CraftCountCriterion)left).ItemsCount;
        }

        public override ushort GetPlayerValue(PlayerAchievement player)
        {
            return (ushort)Math.Min(this.MaxValue, JobManager.Instance.GetAmountOfCraft(player.Owner.Id, ItemId));
        }
    }

}
