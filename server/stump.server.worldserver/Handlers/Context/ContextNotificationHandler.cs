using System;
using System.Collections.Generic;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using System.Linq;

namespace Stump.Server.WorldServer.Handlers.Context
{
    public partial class ContextHandler : WorldHandlerContainer
    {
        public static void SendNotificationListMessage(IPacketReceiver client, IEnumerable<int> notifications)
        {
            client.Send(new NotificationListMessage(notifications.ToArray()));
        }
    }
}