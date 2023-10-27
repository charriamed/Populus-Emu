using System;
using Stump.Core.Reflection;
using Stump.Server.WorldServer.Database.Achievements;
using Stump.Server.WorldServer.Database.Monsters;
using Stump.Server.WorldServer.Game.Achievements.Criterions.Data;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Conditions;

namespace Stump.Server.WorldServer.Game.Achievements.Criterions
{
    public class KillMonsterWithChallengeCriterion :
        AbstractCriterion<KillMonsterWithChallengeCriterion, DefaultCriterionData>
    {
        // FIELDS
        public const string Identifier = "Ef";
        private ushort? m_maxValue;

        // CONSTRUCTORS
        public KillMonsterWithChallengeCriterion(AchievementObjectiveRecord objective)
            : base(objective)
        {
            Monster = Singleton<MonsterManager>.Instance.GetTemplate(MonsterId);
        }

        // PROPERTIES
        public int MonsterId => this[0][0];

        public MonsterTemplate Monster { get; }

        public int Number => this[0][1];

        public override bool IsIncrementable => false;

        public override ushort MaxValue
        {
            get
            {
                if (m_maxValue == null)
                {
                    m_maxValue = (ushort)Number;

                    switch (base[0].Operator)
                    {
                        case ComparaisonOperatorEnum.EQUALS:
                            break;

                        case ComparaisonOperatorEnum.INFERIOR:
                            throw new Exception();

                        case ComparaisonOperatorEnum.SUPERIOR:
                            m_maxValue++;
                            break;
                    }
                }

                return m_maxValue.Value;
            }
        }

        // METHODS
        public override DefaultCriterionData Parse(ComparaisonOperatorEnum @operator, params string[] parameters)
        {
            return new DefaultCriterionData(@operator, parameters);
        }

        public override bool Eval(Character character)
        {
            return character.Achievement.GetRunningCriterion(this) >= Number;
        }

        public override bool Lower(AbstractCriterion left)
        {
            return Number < ((KillMonsterWithChallengeCriterion)left).Number;
        }

        public override bool Greater(AbstractCriterion left)
        {
            return Number > ((KillMonsterWithChallengeCriterion)left).Number;
        }

        public override ushort GetPlayerValue(PlayerAchievement player)
        {
            return (ushort)Math.Min(MaxValue, player.GetRunningCriterion(this));
        }
    }
}