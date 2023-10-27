﻿using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.Interactives;
using Stump.Server.WorldServer.Database.Jobs;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Jobs;
using Stump.DofusProtocol.Types;

namespace Stump.Server.WorldServer.Handlers.Context.RolePlay
{
    public partial class ContextRoleplayHandler : WorldHandlerContainer
    {
        [WorldHandler(JobCrafterDirectoryDefineSettingsMessage.Id)]
        public static void HandleJobCrafterDirectoryDefineSettingsMessage(WorldClient client, JobCrafterDirectoryDefineSettingsMessage message)
        {
            var job = client.Character.Jobs[message.Settings.JobId];
            job.WorkForFree = message.Settings.Free;
            job.MinLevelCraftSetting = message.Settings.MinLevel;

            SendJobCrafterDirectorySettingsMessage(client, client.Character);
            if (job.IsIndexed)
                job.Template.RefreshCrafter(client.Character);
        }

        [WorldHandler(JobBookSubscribeRequestMessage.Id)]
        public static void HandleJobBookSubscribeRequestMessage(WorldClient client, JobBookSubscribeRequestMessage message)
        {
            var subscriptions = new List<JobBookSubscription>();

            foreach (var jobId in message.JobIds)
            {
                var job = client.Character.Jobs[jobId];

                var addedOrRemoved = job.Template.AddOrRemoveAvailableCrafter(client.Character);
                job.IsIndexed = addedOrRemoved;

                subscriptions.Add(new JobBookSubscription((sbyte)job.Template.Id, addedOrRemoved));
            }

            SendJobBookSubscriptionMessage(client, subscriptions.ToArray());
        }

        public static void SendJobMultiCraftAvailableSkillsMessage(IPacketReceiver client, Character character, IEnumerable<InteractiveSkillTemplate> skills, bool enabled)
        {
            client.Send(new JobMultiCraftAvailableSkillsMessage(enabled, (ulong)character.Id, skills.Select(x => (ushort)x.Id).ToArray()));
        }

        public static void SendJobExperienceMultiUpdateMessage(IPacketReceiver client, Character character)
        {
            client.Send(new JobExperienceMultiUpdateMessage(character.Jobs.Where(x => x.Id != 1).Select(x => x.GetJobExperience()).ToArray()));
        }

        public static void SendJobExperienceUpdateMessage(IPacketReceiver client, Job job)
        {
            client.Send(new JobExperienceUpdateMessage(job.GetJobExperience()));
        }

        public static void SendJobExperienceOtherPlayerUpdateMessage(IPacketReceiver client, Character character, Job job)
        {
            client.Send(new JobExperienceOtherPlayerUpdateMessage(job.GetJobExperience(), (ulong)character.Id));
        }

        public static void SendJobDescriptionMessage(IPacketReceiver client, Character character)
        {
            client.Send(new JobDescriptionMessage(character.Jobs.Where(x => x.Id != 1).Select(x => x.GetJobDescription()).ToArray()));
        }

        public static void SendJobCrafterDirectorySettingsMessage(IPacketReceiver client, Character character)
        {
            client.Send(new JobCrafterDirectorySettingsMessage(character.Jobs.Select(x => x.GetJobCrafterDirectorySettings()).ToArray()));
        }

        public static void SendJobLevelUpMessage(IPacketReceiver client, Job job)
        {
            client.Send(new JobLevelUpMessage((byte) job.Level, job.GetJobDescription()));
        }

        public static void SendJobBookSubscriptionMessage(IPacketReceiver client, JobBookSubscription[] subscriptions)
        {
            client.Send(new JobBookSubscriptionMessage(subscriptions));
        }
    }
}