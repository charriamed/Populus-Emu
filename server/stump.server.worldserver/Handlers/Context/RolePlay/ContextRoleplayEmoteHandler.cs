using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.Extensions;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game.Actors;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Handlers.Context.RolePlay
{
    public partial class ContextRoleplayHandler : WorldHandlerContainer
    {
        [WorldHandler(EmotePlayRequestMessage.Id)]
        public static void HandleEmotePlayRequestMessage(WorldClient client, EmotePlayRequestMessage message)
        {
            if (!client.Character.HasEmote((EmotesEnum) message.EmoteId))
                return;

            client.Character.PlayEmote((EmotesEnum) message.EmoteId);
        }

        public static void SendEmotePlayMessage(IPacketReceiver client, Character character, EmotesEnum emote)
        {
            client.Send(new EmotePlayMessage(
                            (byte) emote,
                            DateTime.Now.GetUnixTimeStampLong(),
                            character.Id,
                            character.Account.Id
                            ));
        }

        public static void SendEmotePlayMessage(IPacketReceiver client, ContextActor actor, EmotesEnum emote)
        {
            client.Send(new EmotePlayMessage(
                            (byte) emote,
                            DateTime.Now.GetUnixTimeStampLong(),
                            actor.Id,
                            0
                            ));
        }

        public static void SendEmoteListMessage(IPacketReceiver client, IEnumerable<sbyte> emoteList)
        {
            client.Send(new EmoteListMessage(emoteList.Select(x=>(byte)x).ToArray()));
        }

        public static void SendEmoteAddMessage(IPacketReceiver client, sbyte emote)
        {
            client.Send(new EmoteAddMessage((byte)emote));
        }

        public static void SendEmoteRemoveMessage(IPacketReceiver client, sbyte emote)
        {
            client.Send(new EmoteRemoveMessage((byte)emote));
        }

        public static void SendEmotePlayErrorMessage(IPacketReceiver client, sbyte emote)
        {
            client.Send(new EmotePlayErrorMessage((byte)emote));
        }
    }
}