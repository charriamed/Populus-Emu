using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game.Spells;

namespace Stump.Server.WorldServer.Handlers.Inventory
{
    public partial class InventoryHandler : WorldHandlerContainer
    {
        public static void SendSpellListMessage(WorldClient client, bool previsualization)
        {
            var list = client.Character.Spells.GetPlayableSpells().Where(x => x.Record.Position == 63).Select(entry => entry.GetSpellItem()).ToArray();
            client.Send(new SpellListMessage(previsualization, list));
        }
    }
}