
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Core.Network;

namespace Stump.Server.WorldServer.Handlers.Actions
{
    public partial class ActionsHandler : WorldHandlerContainer
    {
        [WorldHandler(GameActionAcknowledgementMessage.Id)]
        public static void HandleGameActionAcknowledgementMessage(WorldClient client, GameActionAcknowledgementMessage message)
        {
            // valid == true anyway
            if (message.Valid && client.Character.IsFighting() && client.Character.Fighter.IsFighterTurn())
            {
                client.Character.Fighter.Fight.AcknowledgeAction(client.Character.Fighter, message.ActionId);
            }
        }
    }
}