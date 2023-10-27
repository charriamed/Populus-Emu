using System.Collections.Generic;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game.Achievements;
using Stump.Server.WorldServer.Handlers.Characters;
using System.Linq;
using Stump.Server.WorldServer.Database.Achievements;

namespace Stump.Server.WorldServer.Handlers.Achievements
{
    public class AchievementHandler : WorldHandlerContainer
    {
        private AchievementHandler()
        {
        }

        [WorldHandler(AchievementDetailedListRequestMessage.Id)]
        public static void HandleAchievementDetailedListRequestMessage(WorldClient client,
            AchievementDetailedListRequestMessage message)
        {
            var category = Singleton<AchievementManager>.Instance.TryGetAchievementCategory((uint)message.CategoryId);
            if (category != null)
                SendAchievementDetailedListMessage(client, new Achievement[0],
                    client.Character.Achievement.TryGetFinishedAchievements(category));
        }

        [WorldHandler(AchievementDetailsRequestMessage.Id)]
        public static void HandleAchievementDetailsRequestMessage(WorldClient client,
            AchievementDetailsRequestMessage message)
        {
            var template = Singleton<AchievementManager>.Instance.TryGetAchievement((uint)message.AchievementId);
            if (template != null)
                SendAchievementDetailsMessage(client, template.GetAchievement(client.Character.Achievement));
        }

        [WorldHandler(DareRewardConsumeRequestMessage.Id)]
        public static void HandleDareRewardConsumeRequestMessage(WorldClient client, DareRewardConsumeRequestMessage message)
        {
            List<AchievementTemplate> Achievements = new List<AchievementTemplate>();
            foreach(var achiev in client.Character.Achievement.FinishedAchievements)
            {
                Achievements.Add(achiev);
            }
            foreach (var Achievement in Achievements)
            {
                var achievement = client.Character.Achievement.TryGetFinishedAchievement((short)Achievement.Id);
                if (achievement != null)
                {
                    int experience;
                    int guildExperience;
                    if (client.Character.Achievement.RewardAchievement(achievement, out experience, out guildExperience))
                    {
                        SendAchievementRewardSuccessMessage(client, (short)Achievement.Id);

                        if (experience > 0)
                            client.Character.AddExperience(experience);
                        else
                            experience = 0;

                        if (client.Character.GuildMember != null && guildExperience > 0)
                            client.Character.GuildMember.AddXP(guildExperience);
                        else
                            guildExperience = 0;

                        CharacterHandler.SendCharacterExperienceGainMessage(client, (ulong)experience, 0L,
                            (ulong)guildExperience, 0L);

                        if(client.Character.Record.UnAcceptedAchievements.Contains((ushort)achievement.Id))
                            client.Character.Record.UnAcceptedAchievements.Remove((ushort)achievement.Id);
                    }
                    else
                        SendAchievementRewardErrorMessage(client, (short)Achievement.Id);
                }
                else
                    SendAchievementRewardErrorMessage(client, (short)Achievement.Id);
            }
        }

        [WorldHandler(AchievementRewardRequestMessage.Id)]
        public static void HandleAchievementRewardRequestMessage(WorldClient client,
            AchievementRewardRequestMessage message)
        {
            if (message.AchievementId > 0)
            {
                var achievement = client.Character.Achievement.TryGetFinishedAchievement(message.AchievementId);
                if (achievement != null)
                {
                    int experience;
                    int guildExperience;
                    if (client.Character.Achievement.RewardAchievement(achievement, out experience, out guildExperience))
                    {
                        SendAchievementRewardSuccessMessage(client, message.AchievementId);

                        if (experience > 0)
                            client.Character.AddExperience(experience);
                        else
                            experience = 0;

                        if (client.Character.GuildMember != null && guildExperience > 0)
                            client.Character.GuildMember.AddXP(guildExperience);
                        else
                            guildExperience = 0;

                        CharacterHandler.SendCharacterExperienceGainMessage(client, (ulong)experience, 0L,
                            (ulong)guildExperience, 0L);
                        if (client.Character.Record.UnAcceptedAchievements.Contains((ushort)achievement.Id))
                            client.Character.Record.UnAcceptedAchievements.Remove((ushort)achievement.Id);
                    }
                    else
                        SendAchievementRewardErrorMessage(client, message.AchievementId);
                }
                else
                    SendAchievementRewardErrorMessage(client, message.AchievementId);
            }
        }

        [WorldHandler(StartupActionsAllAttributionMessage.Id)]
        public static void HandleStartupActionsAllAttributionMessage(WorldClient client,
            StartupActionsAllAttributionMessage message)
        {
            if (client.LastMessage is AchievementRewardRequestMessage)
            {
                client.Character.Achievement.RewardAllAchievements((achievement, success) =>
                {
                    if (success)
                        SendAchievementRewardSuccessMessage(client, (short)achievement.Id);
                    else
                        SendAchievementRewardErrorMessage(client, (short)achievement.Id);
                });
            }
        }

        public static void SendAchievementListMessage(IPacketReceiver client,
            IEnumerable<ushort> finishedAchievementsIds, ulong charId)
        {
            var AchievementsAchieved = new List<AchievementAchieved>();
            foreach(var finishedAchievements in finishedAchievementsIds)
            {
                AchievementsAchieved.Add(new AchievementAchieved(finishedAchievements, charId));
            }
            
            client.Send(new AchievementListMessage(AchievementsAchieved.ToArray()));
        }

        public static void SendAchievementDetailedListMessage(IPacketReceiver client,
            IEnumerable<Achievement> startedAchievements, IEnumerable<Achievement> finishedAchievements)
        {
            client.Send(new AchievementDetailedListMessage(startedAchievements.ToArray(), finishedAchievements.ToArray()));
        }

        public static void SendAchievementDetailsMessage(IPacketReceiver client, Achievement achievement)
        {
            client.Send(new AchievementDetailsMessage(achievement));
        }

        public static void SendAchievementFinishedMessage(IPacketReceiver client, ushort id, ulong charId, ushort finishedLevel)
        {
            if (finishedLevel > 200)
                finishedLevel = 200;
            client.Send(new AchievementFinishedMessage(new AchievementAchievedRewardable(id, charId, finishedLevel)));
        }

        public static void SendAchievementRewardSuccessMessage(IPacketReceiver client, short achievementId)
        {
            client.Send(new AchievementRewardSuccessMessage(achievementId));
        }

        public static void SendAchievementRewardErrorMessage(IPacketReceiver client, short achievementId)
        {
            client.Send(new AchievementRewardErrorMessage(achievementId));
        }
    }
}