using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Core.Network;
using System.Linq;

namespace Stump.Server.WorldServer.Handlers.FinishMove
{
    public class FinishMoveHandler : WorldHandlerContainer
    {
        [WorldHandler(FinishMoveListRequestMessage.Id)]
        public static void HandleFinishMoveListRequestMessage(WorldClient client, FinishMoveListRequestMessage message)
        {
            SendFinishMoveListMessage(client);
        }

        [WorldHandler(FinishMoveSetRequestMessage.Id)]
        public static void HandleFinishMoveSetRequestMessage(WorldClient client, FinishMoveSetRequestMessage message)
        {
            var finishMove = client.Character.GetFinishMove(message.FinishMoveId);

            if (finishMove == null)
                return;

            finishMove.State = message.FinishMoveState;
        }

        public static void SendFinishMoveListMessage(WorldClient client)
        {
            var informations = client.Character.GetFinishMovesInformations();

            if (!informations.Any())
                return;

            client.Send(new FinishMoveListMessage(informations));
        }
    }
}
