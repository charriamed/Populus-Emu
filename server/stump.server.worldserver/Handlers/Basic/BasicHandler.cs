using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.Extensions;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game;

namespace Stump.Server.WorldServer.Handlers.Basic
{
    public class BasicHandler : WorldHandlerContainer
    {
        /*[WorldHandler(BasicSetAwayModeRequestMessage.Id)]
        public static void HandleBasicSwitchModeRequestMessage(WorldClient client, BasicSetAwayModeRequestMessage message)
        {
            client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, client.Character.ToggleAway() ? (short) 37 : (short) 38);
        }*/

        [WorldHandler(BasicWhoAmIRequestMessage.Id)]
        public static void HandleBasicWhoAmIRequestMessage(WorldClient client, BasicWhoAmIRequestMessage message)
        {
            /* Get Current character */
            var character = client.Character;

            /* Send informations about it */
            client.Send(new BasicWhoIsMessage(
                character == client.Character,
                message.Verbose,
                (sbyte)character.UserGroup.Role,
                character.Client.WorldAccount.Nickname,
                character.Account.Id,
                character.Name,
                (ulong)character.Id,
                (short)character.Map.Area.Id,
                (short)WorldServer.ServerInformation.Id,
                (short)WorldServer.ServerInformation.Id,
                character.GuildMember == null ? new AbstractSocialGroupInfos[0] : new[] { character.Guild.GetBasicGuildInformations() },
                character.IsInFight() ? (sbyte)PlayerStateEnum.GAME_TYPE_FIGHT : (sbyte)PlayerStateEnum.GAME_TYPE_ROLEPLAY));
        }

        [WorldHandler(BasicWhoIsRequestMessage.Id)]
        public static void HandleBasicWhoIsRequestMessage(WorldClient client, BasicWhoIsRequestMessage message)
        {
            /* Get character */
            var character = World.Instance.GetCharacter(message.Search);

            /* check null */
            if (character == null)
            {
                client.Send(new BasicWhoIsNoMatchMessage(message.Search));
            }
            else
            {
                /* Send info about it */
                client.Send(new BasicWhoIsMessage(
                    character == client.Character,
                    message.Verbose,
                    (sbyte)character.UserGroup.Role,
                    character.Client.WorldAccount.Nickname,
                    character.Account.Id,
                    character.Name,
                    (ulong)character.Id,
                    (short)character.Map.SubArea.Id,
                    (short)WorldServer.ServerInformation.Id,
                    (short)WorldServer.ServerInformation.Id,
                    character.GuildMember == null ? new AbstractSocialGroupInfos[0] : new[] { character.Guild.GetBasicGuildInformations() },
                    character.IsInFight() ? (sbyte)PlayerStateEnum.GAME_TYPE_FIGHT : (sbyte)PlayerStateEnum.GAME_TYPE_ROLEPLAY));
            }
        }

        [WorldHandler(NumericWhoIsRequestMessage.Id)]
        public static void HandleNumericWhoIsRequestMessage(WorldClient client, NumericWhoIsRequestMessage message)
        {
            /* Get character */
            var character = World.Instance.GetCharacter((int)message.PlayerId);

            /* check null */
            if (character != null)
            {
                /* Send info about it */
                client.Send(new NumericWhoIsMessage((ulong)character.Id, character.Account.Id));
            }
        }

        public static void SendTextInformationMessage(IPacketReceiver client, TextInformationTypeEnum msgType, short msgId,
                                                      params string[] arguments)
        {
            client.Send(new TextInformationMessage((sbyte) msgType, (ushort)msgId, arguments));
        }

        public static void SendTextInformationMessage(IPacketReceiver client, TextInformationTypeEnum msgType, short msgId,
                                                      params object[] arguments)
        {
            client.Send(new TextInformationMessage((sbyte) msgType, (ushort)msgId, arguments.Select(entry => entry.ToString()).ToArray()));
        }

        public static void SendTextInformationMessage(IPacketReceiver client, TextInformationTypeEnum msgType, short msgId)
        {
            client.Send(new TextInformationMessage((sbyte) msgType, (ushort)msgId, new string[0]));
        }

        public static void SendSystemMessageDisplayMessage(IPacketReceiver client, bool hangUp, short msgId, IEnumerable<string> arguments)
        {
            client.Send(new SystemMessageDisplayMessage(hangUp, (ushort)msgId, arguments.ToArray()));
        }

        public static void SendSystemMessageDisplayMessage(IPacketReceiver client, bool hangUp, short msgId, params object[] arguments)
        {
            client.Send(new SystemMessageDisplayMessage(hangUp, (ushort)msgId, arguments.Select(entry => entry.ToString()).ToArray()));
        }

        public static void SendBasicTimeMessage(IPacketReceiver client)
        {
            var offset = (short) TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).TotalMinutes;
            client.Send(new BasicTimeMessage(DateTime.Now.GetUnixTimeStampLong(), offset));
        }

        public static void SendBasicNoOperationMessage(IPacketReceiver client)
        {
            client.Send(new BasicNoOperationMessage());
        }

        public static void SendCinematicMessage(IPacketReceiver client, short cinematicId)
        {
            client.Send(new CinematicMessage((ushort)cinematicId));
        }

        public static void SendServerExperienceModificatorMessage(IPacketReceiver client)
        {
            client.Send(new ServerExperienceModificatorMessage((ushort)(Rates.XpRate * 100)));
        }
    }
}