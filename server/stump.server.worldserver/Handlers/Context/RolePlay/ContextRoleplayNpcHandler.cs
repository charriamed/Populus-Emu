using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Game.Actors.Interfaces;
using Stump.Server.WorldServer.Game.Actors.RolePlay;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Exchanges.BidHouse;

namespace Stump.Server.WorldServer.Handlers.Context.RolePlay
{
    public partial class ContextRoleplayHandler : WorldHandlerContainer
    {
        [WorldHandler(NpcGenericActionRequestMessage.Id)]
        public static void HandleNpcGenericActionRequestMessage(WorldClient client, NpcGenericActionRequestMessage message)
        {
            if (client.Character.Dialog != null && (client.Character.Dialog as BidHouseExchange)?.Npc == null)
            {
                var bidbuy = new BidHouseExchange(client.Character, (client.Character.Dialog as BidHouseExchange).Types, (client.Character.Dialog as BidHouseExchange).MaxItemLevel, !(client.Character.Dialog as BidHouseExchange).Buy);
                bidbuy.Open();
            }
            else
            {
                var npc = client.Character.Map.GetActor<RolePlayActor>(message.NpcId) as IInteractNpc;

                if (npc == null)
                    return;

                npc.InteractWith((NpcActionTypeEnum)message.NpcActionId, client.Character);
            }
        }

        [WorldHandler(NpcDialogReplyMessage.Id)]
        public static void HandleNpcDialogReplyMessage(WorldClient client, NpcDialogReplyMessage message)
        {
            client.Character.ReplyToNpc((int)message.ReplyId);
        }

        public static void SendNpcDialogCreationMessage(IPacketReceiver client, Npc npc)
        {
            client.Send(new NpcDialogCreationMessage(npc.Position.Map.Id, npc.Id));
        }

        public static void SendNpcDialogQuestionMessage(IPacketReceiver client, NpcMessage message, IEnumerable<short> replies, params string[] parameters)
        {
            client.Send(new NpcDialogQuestionMessage((ushort)message.Id, parameters, replies.Select(x => (uint)x).ToArray()));
        }
    }
}