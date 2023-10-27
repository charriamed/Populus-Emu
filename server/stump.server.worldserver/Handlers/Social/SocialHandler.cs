using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Handlers.Social
{
    public class SocialHandler : WorldHandlerContainer
    {
        [WorldHandler(ContactLookRequestByIdMessage.Id)]
        public static void HandleContactLookRequestByIdMessage(WorldClient client, ContactLookRequestByIdMessage message)
        {
            var target = World.Instance.GetCharacter((int) message.PlayerId);

            if (target != null)
                SendContactLookMessage(client, message.RequestId, target);
            else
                SendContactLookErrorMessage(client, message.RequestId);
        }

        [WorldHandler(ContactLookRequestByNameMessage.Id)]
        public static void HandleContactLookRequestByNameMessage(WorldClient client, ContactLookRequestByNameMessage message)
        {
            var target = World.Instance.GetCharacter(message.PlayerName);

            if (target != null)
                SendContactLookMessage(client, message.RequestId, target);
            else
                SendContactLookErrorMessage(client, message.RequestId);
        }

        public static void SendContactLookMessage(IPacketReceiver client, int requestId, Character character)
        {
            client.Send(new ContactLookMessage((uint)requestId, character.Name, (ulong)character.Id, character.Look.GetEntityLook()));
        }

        public static void SendContactLookErrorMessage(IPacketReceiver client, int requestId)
        {
            client.Send(new ContactLookErrorMessage((uint)requestId));
        }
    }
}