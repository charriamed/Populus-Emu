using Stump.Server.WorldServer.Database.Achievements;
using Stump.Server.WorldServer.Game.Achievements.Criterions.Data;
using Stump.Server.WorldServer.Game.Conditions;

namespace Stump.Server.WorldServer.Game.Achievements.Criterions
{
    public sealed class AchievementCriterion : AbstractCriterion<AchievementCriterion, AchievementCriterionData>
    {
        // FIELDS
        public const string Identifier = "OA";

        // PROPERTIES
        public AchievementTemplate Achievement
        {
            get
            {
                return base[0].Achievement;
            }
        }

        // CONSTRUCTORS
        public AchievementCriterion(AchievementObjectiveRecord objective)
            : base(objective)
        { }

        // METHODS
        public override AchievementCriterionData Parse(ComparaisonOperatorEnum @operator, params string[] parameters)
        {
            return new AchievementCriterionData(@operator, parameters);
        }
    }
}
