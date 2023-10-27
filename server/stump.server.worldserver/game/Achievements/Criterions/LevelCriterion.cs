using Stump.Server.WorldServer.Database.Achievements;
using Stump.Server.WorldServer.Game.Achievements.Criterions.Data;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Conditions;
using System;

namespace Stump.Server.WorldServer.Game.Achievements.Criterions
{
    public class LevelCriterion : AbstractCriterion<LevelCriterion, DefaultCriterionData>
    {
        // FIELDS
        public const string Identifier = "PL";

        // PROPERTIES
        public int Level
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

        // CONSTRUCTORS
        public LevelCriterion(AchievementObjectiveRecord objective)
            : base(objective)
        { }

        // METHODS
        public override DefaultCriterionData Parse(ComparaisonOperatorEnum @operator, params string[] parameters)
        {
            return new DefaultCriterionData(@operator, parameters);
        }

        public override bool Eval(Character character)
        {
            return this.Level < character.Level;
        }

        public override bool Lower(AbstractCriterion left)
        {
            return this.Level < ((LevelCriterion)left).Level;
        }
        public override bool Greater(AbstractCriterion left)
        {
            return this.Level > ((LevelCriterion)left).Level;
        }

        public override ushort GetPlayerValue(PlayerAchievement player)
        {
            return (ushort)Math.Min(base.MaxValue, player.GetRunningCriterion(this));
        }
    }
}
