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
    public class IdolsScoreCriterion :
        AbstractCriterion<IdolsScoreCriterion, DefaultCriterionData>
    {
        // FIELDS
        public const string Identifier = "Ei";
        private ushort? m_maxValue;

        // CONSTRUCTORS
        public IdolsScoreCriterion(AchievementObjectiveRecord objective)
            : base(objective)
        {
            Monster = Singleton<MonsterManager>.Instance.GetTemplate(MonsterId);
        }

        // PROPERTIES
        public int MonsterId => this[0][0];

        public MonsterTemplate Monster { get; }

        public int Score => this[0][1];

        public override bool IsIncrementable => false;

        // METHODS
        public override DefaultCriterionData Parse(ComparaisonOperatorEnum @operator, params string[] parameters)
        {
            return new DefaultCriterionData(@operator, parameters);
        }

        public override bool Eval(Character character)
        {
            return character.Fighter.Fight.GetIdolsScore() >= Score;
        }

        public override bool Lower(AbstractCriterion left)
        {
            return Score < ((IdolsScoreCriterion)left).Score;
        }

        public override bool Greater(AbstractCriterion left)
        {
            return Score > ((IdolsScoreCriterion)left).Score;
        }

        public override ushort GetPlayerValue(PlayerAchievement player)
        {
            return (ushort)Math.Min(0, player.GetRunningCriterion(this));
        }
    }
}