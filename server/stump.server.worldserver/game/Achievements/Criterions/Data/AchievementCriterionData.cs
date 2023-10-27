using Stump.Core.Reflection;
using Stump.Server.WorldServer.Database.Achievements;
using Stump.Server.WorldServer.Game.Conditions;
using System;

namespace Stump.Server.WorldServer.Game.Achievements.Criterions.Data
{
    public class AchievementCriterionData : CriterionData
    {
        // FIELDS
        private AchievementTemplate m_achievement;
        private uint m_achievementId;

        // PROPERTIES
        public AchievementTemplate Achievement
        {
            get
            {
                return this.m_achievement;
            }
        }

        // CONSTRUCTORS
        public AchievementCriterionData(ComparaisonOperatorEnum @operator, params string[] parameters)
            : base(@operator, parameters)
        {
            if (uint.TryParse(base[0], out this.m_achievementId))
            {
                this.m_achievement = Singleton<AchievementManager>.Instance.TryGetAchievement(this.m_achievementId);
            }
            else
            {
                throw new Exception();
            }
        }

        // METHODS

    }
}
