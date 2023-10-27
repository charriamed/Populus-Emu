using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Shortcut = Stump.Server.WorldServer.Database.Shortcuts.Shortcut;

namespace Stump.Server.WorldServer.Handlers.Shortcuts
{
    public class ShortcutHandler : WorldHandlerContainer
    {
        [WorldHandler(ShortcutBarAddRequestMessage.Id)]
        public static void HandleShortcutBarAddRequestMessage(WorldClient client, ShortcutBarAddRequestMessage message)
        {
            client.Character.Shortcuts.AddShortcut((ShortcutBarEnum) message.BarType, message.Shortcut);
        }

        [WorldHandler(ShortcutBarRemoveRequestMessage.Id)]
        public static void HandleShortcutBarRemoveRequestMessage(WorldClient client, ShortcutBarRemoveRequestMessage message)
        {
            client.Character.Shortcuts.RemoveShortcut((ShortcutBarEnum)message.BarType, message.Slot);
        }

        [WorldHandler(ShortcutBarSwapRequestMessage.Id)]
        public static void HandleShortcutBarSwapRequestMessage(WorldClient client, ShortcutBarSwapRequestMessage message)
        {
            client.Character.Shortcuts.SwapShortcuts((ShortcutBarEnum)message.BarType, message.FirstSlot, message.SecondSlot);
        }

        public static void SendShortcutBarContentMessage(WorldClient client, ShortcutBarEnum barType)
        {
            client.Send(new ShortcutBarContentMessage((sbyte)barType,
                client.Character.Shortcuts.GetShortcuts(barType).Select(entry => entry.GetNetworkShortcut()).ToArray()));
        }

        public static void SendShortcutBarContentMessage(WorldClient client, IEnumerable<DofusProtocol.Types.Shortcut> shortcuts, ShortcutBarEnum barType)
        {
            client.Send(new ShortcutBarContentMessage((sbyte)barType, shortcuts.ToArray()));
        }

        public static void SendShortcutBarRefreshMessage(IPacketReceiver client, ShortcutBarEnum barType, Shortcut shortcut)
        {
            client.Send(new ShortcutBarRefreshMessage((sbyte)barType, shortcut.GetNetworkShortcut()));
        }

        public static void SendShortcutBarRemovedMessage(IPacketReceiver client, ShortcutBarEnum barType, int slot)
        {
            client.Send(new ShortcutBarRemovedMessage((sbyte)barType, (sbyte)slot));
        }

        public static void SendShortcutBarRemoveErrorMessage(IPacketReceiver client)
        {
            client.Send(new ShortcutBarRemoveErrorMessage());
        }

        public static void SendShortcutBarSwapErrorMessage(IPacketReceiver client)
        {
            client.Send(new ShortcutBarSwapErrorMessage());
        }

        public static void SendShortcutBarAddErrorMessage(IPacketReceiver client)
        {
            client.Send(new ShortcutBarAddErrorMessage());
        }
    }
}