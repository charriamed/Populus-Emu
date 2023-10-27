using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;

namespace Stump.Server.WorldServer.Handlers.Inventory
{
    public partial class InventoryHandler : WorldHandlerContainer
    {
        public static void SendKamasUpdateMessage(IPacketReceiver client, ulong kamasAmount)
        {
            client.Send(new KamasUpdateMessage(kamasAmount));
        }
    }
}