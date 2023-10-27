using System.Collections.Generic;
using Stump.DofusProtocol.Enums.Custom;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game.Fights.Challenges;
using System.Linq;

namespace Stump.Server.WorldServer.Handlers.Context
{
    public partial class ContextHandler : WorldHandlerContainer
    {
        [WorldHandler(ChallengeTargetsListRequestMessage.Id)]
        public static void HandleChallengeTargetsListRequestMessage(WorldClient client, ChallengeTargetsListRequestMessage message)
        {
            if (!client.Character.IsFighting())
                return;

            var challenge = client.Character.Fight.Challenges.FirstOrDefault(x => x.Id == message.ChallengeId);

            if (challenge?.Target == null)
                return;

            if (!challenge.Target.IsVisibleFor(client.Character))
                return;

            SendChallengeTargetsListMessage(challenge.Fight.Clients, new[] { (double)challenge.Target.Id }, new[] { challenge.Target.Cell.Id });
        }

        public static void SendChallengeInfoMessage(IPacketReceiver client, DefaultChallenge challenge)
        {
            client.Send(new ChallengeInfoMessage((ushort)challenge.Id, challenge.Target != null ? challenge.Target.Id : -1, (uint)challenge.Bonus, (uint)challenge.Bonus));
        }

        public static void SendChallengeResultMessage(IPacketReceiver client, DefaultChallenge challenge)
        {
            client.Send(new ChallengeResultMessage((ushort)challenge.Id, challenge.Status == ChallengeStatusEnum.SUCCESS));
        }

        public static void SendChallengeTargetsListMessage(IPacketReceiver client, IEnumerable<double> targetIds, IEnumerable<short> targetCells)
        {
            client.Send(new ChallengeTargetsListMessage(targetIds.ToArray(), targetCells.ToArray()));
        }
    }
}
