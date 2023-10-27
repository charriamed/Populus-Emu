using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using System.Linq;

namespace Stump.Server.WorldServer.Handlers.Chat
{
    public partial class ChatHandler : WorldHandlerContainer
    {
        [WorldHandler(ChannelEnablingMessage.Id)]
        public static void HandleChannelEnablingMessage(WorldClient client, ChannelEnablingMessage message)
        {
        }

        public static void SendEnabledChannelsMessage(IPacketReceiver client, sbyte[] allows, sbyte[] disallows)
        {
            client.Send(new EnabledChannelsMessage(allows.Select(x => (byte)x).ToArray(), disallows.Select(x => (byte)x).ToArray()));
        }
    }
}