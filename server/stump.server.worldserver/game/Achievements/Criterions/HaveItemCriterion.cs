using System;
using System.Linq;
using Stump.Core.Reflection;
using Stump.Server.WorldServer.Database.Achievements;
using Stump.Server.WorldServer.Database.Monsters;
using Stump.Server.WorldServer.Game.Achievements.Criterions.Data;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Conditions;

namespace Stump.Server.WorldServer.Game.Achievements.Criterions
{
    public class HaveItemCriterion :
        AbstractCriterion<HaveItemCriterion, DefaultCriterionData>
    {
        // FIELDS
        public const string Identifier = "PO";

        // CONSTRUCTORS
        public HaveItemCriterion(AchievementObjectiveRecord objective)
            : base(objective)
        {
        }

        // PROPERTIES
        public int ItemId => this[0][0];

        public override bool IsIncrementable => false;

        // METHODS
        public override DefaultCriterionData Parse(ComparaisonOperatorEnum @operator, params string[] parameters)
        {
            return new DefaultCriterionData(@operator, parameters);
        }

        public override bool Eval(Character character)
        {
            return character.Inventory.Any(x => x.Template.Id == ItemId);
        }

        public override ushort GetPlayerValue(PlayerAchievement player)
        {
            return (ushort)Math.Min(0, player.GetRunningCriterion(this));
        }
    }
}