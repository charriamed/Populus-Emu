using Stump.Core.Reflection;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Achievements;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Achievements
{
    [Serializable]
    public class PlayerAchievementReward
    {
        // FIELDS
        private ushort m_rewardLevel;
        [NonSerialized] private List<AchievementTemplate> m_rewardAchievements;
        private List<ushort> m_rewardAchievementIds;
        [NonSerialized] private Character m_owner;

        // PROPERTIES
        public Character Owner
        {
            get
            {
                return this.m_owner;
            }
            private set
            {
                this.m_owner = value;
            }
        }
        public IReadOnlyList<AchievementTemplate> RewardAchievements
        {
            get
            {
                return this.m_rewardAchievements.AsReadOnly();
            }
        }

        // CONSTRUCTORS
        public PlayerAchievementReward(Character owner, byte rewardLevel, params ushort[] achievementIds)
        {
            this.m_rewardAchievements = new List<AchievementTemplate>();
            this.m_rewardAchievementIds = new List<ushort>();

            this.m_owner = owner;

            this.m_rewardLevel = rewardLevel;
            foreach (var item in achievementIds)
            {
                var achievement = Singleton<AchievementManager>.Instance.TryGetAchievement(item);
                if (achievement != null)
                {
                    this.m_rewardAchievements.Add(achievement);
                    this.m_rewardAchievementIds.Add((ushort)achievement.Id);
                }
            }
        }
        public PlayerAchievementReward(Character owner, params AchievementTemplate[] achievements)
        {
            this.m_rewardAchievements = new List<AchievementTemplate>();
            this.m_rewardAchievementIds = new List<ushort>();

            this.m_owner = owner;

            this.m_rewardLevel = owner.Level;

            foreach (var item in achievements)
            {
                this.AddRewardableAchievement(item);
            }
        }

        // METHODS
        public void Initialize(Character owner)
        {
            this.m_rewardAchievements = new List<AchievementTemplate>();

            this.m_owner = owner;
            foreach (var item in this.m_rewardAchievementIds)
            {
                var achievement = Singleton<AchievementManager>.Instance.TryGetAchievement(item);
                if (achievement != null)
                {
                    this.m_rewardAchievements.Add(achievement);
                }
            }
        }

        public IEnumerable<AchievementAchievedRewardable> GetRewardableAchievements()
        {
            foreach (var item in this.m_rewardAchievements)
            {
                yield return new AchievementAchievedRewardable((ushort)item.Id, (ulong)Owner.Id, this.m_rewardLevel);
            }

            yield break;
        }

        public void AddRewardableAchievement(AchievementTemplate achievement)
        {
            this.m_rewardAchievements.Add(achievement);
            this.m_rewardAchievementIds.Add((ushort)achievement.Id);
        }

        public bool Contains(AchievementTemplate achievement)
        {
            return this.m_rewardAchievements.Contains(achievement);
        }
        public bool Remove(AchievementTemplate achievement)
        {
            if (this.m_rewardAchievementIds.Remove((ushort)achievement.Id))
            {
                return this.m_rewardAchievements.Remove(achievement);
            }

            return false;
        }

        public bool Any()
        {
            return this.m_rewardAchievements.Any();
        }

        public static bool operator ==(PlayerAchievementReward achievement, byte level)
        {
            return achievement.m_rewardLevel == level;
        }
        public static bool operator !=(PlayerAchievementReward achievement, byte level)
        {
            return achievement.m_rewardLevel != level;
        }
    }
}
