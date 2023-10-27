using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game.Accounts;
using Stump.Server.WorldServer.Game.Social;
using Stump.Server.WorldServer.Game.Guilds;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using System.Drawing;
using Stump.Server.WorldServer.Game;

namespace Stump.Server.WorldServer.Handlers.Friends
{
    public class FriendHandler : WorldHandlerContainer
    {
        [WorldHandler(FriendsGetListMessage.Id)]
        public static void HandleFriendsGetListMessage(WorldClient client, FriendsGetListMessage message)
        {
            SendFriendsListMessage(client, client.Character.FriendsBook.Friends);

            SendGuildListMessage(client);
            SendGuildVersatileInfoListMessage(client);
        }

        [WorldHandler(IgnoredGetListMessage.Id)]
        public static void HandleIgnoredGetListMessage(WorldClient client, IgnoredGetListMessage message)
        {
            SendIgnoredListMessage(client, client.Character.FriendsBook.Ignoreds);
        }

        [WorldHandler(FriendAddRequestMessage.Id)]
        public static void HandleFriendAddRequestMessage(WorldClient client, FriendAddRequestMessage message)
        {
            var character = Game.World.Instance.GetCharacter(message.Name);

            if (character != null)
            {
                if (character.UserGroup.Role == RoleEnum.Player)
                    client.Character.FriendsBook.AddFriend(character.Client.WorldAccount);
                else
                    SendFriendAddFailureMessage(client, ListAddFailureEnum.LIST_ADD_FAILURE_NOT_FOUND);
            }
            else
            {
                WorldServer.Instance.IOTaskPool.AddMessage(
                    () =>
                        {
                            var record = AccountManager.Instance.FindByNickname(message.Name);

                            if (record != null && client.Character.Context != null)
                            {
                                client.Character.Context.ExecuteInContext(
                                    () => client.Character.FriendsBook.AddFriend(record));
                            }
                            else
                            {
                                SendFriendAddFailureMessage(client, ListAddFailureEnum.LIST_ADD_FAILURE_NOT_FOUND);
                            }
                        });
            }
        }

        [WorldHandler(FriendDeleteRequestMessage.Id)]
        public static void HandleFriendDeleteRequestMessage(WorldClient client, FriendDeleteRequestMessage message)
        {
            var friend = client.Character.FriendsBook.Friends.FirstOrDefault(entry => entry.Account.Id == message.AccountId);

            if (friend == null)
            {
                SendFriendDeleteResultMessage(client, false, "");
                return;
            }

            client.Character.FriendsBook.RemoveFriend(friend);
        }

        [WorldHandler(IgnoredAddRequestMessage.Id)]
        public static void HandleIgnoredAddRequestMessage(WorldClient client, IgnoredAddRequestMessage message)
        {
            var character = Game.World.Instance.GetCharacter(message.Name);

            if (character != null)
            {
                if (character.UserGroup.Role == RoleEnum.Player)
                    client.Character.FriendsBook.AddIgnored(character.Client.WorldAccount, message.Session);
                else
                    SendFriendAddFailureMessage(client, ListAddFailureEnum.LIST_ADD_FAILURE_NOT_FOUND);
            }
            else
            {
                WorldServer.Instance.IOTaskPool.AddMessage(
                    () =>
                    {
                        var record = AccountManager.Instance.FindByNickname(message.Name);

                        if (record != null && client.Character.Context != null)
                        {
                            client.Character.Context.ExecuteInContext(
                                () => client.Character.FriendsBook.AddIgnored(record, message.Session));
                        }
                        else
                        {
                            SendIgnoredAddFailureMessage(client, ListAddFailureEnum.LIST_ADD_FAILURE_NOT_FOUND);
                        }
                    });
            }
        }

        [WorldHandler(IgnoredDeleteRequestMessage.Id)]
        public static void HandleIgnoredDeleteRequestMessage(WorldClient client, IgnoredDeleteRequestMessage message)
        {
            var ignored = client.Character.FriendsBook.Ignoreds.FirstOrDefault(entry => entry.Account.Id == message.AccountId);

            if (ignored == null)
            {
                SendIgnoredDeleteResultMessage(client, false, false, "");
                return;
            }

            client.Character.FriendsBook.RemoveIgnored(ignored);
        }

        [WorldHandler(FriendSetWarnOnConnectionMessage.Id)]
        public static void HandleFriendSetWarnOnConnectionMessage(WorldClient client, FriendSetWarnOnConnectionMessage message)
        {
            client.Character.FriendsBook.WarnOnConnection = message.Enable;
        }

        [WorldHandler(FriendWarnOnLevelGainStateMessage.Id)]
        public static void HandleFriendWarnOnLevelGainStateMessage(WorldClient client, FriendWarnOnLevelGainStateMessage message)
        {
            client.Character.FriendsBook.WarnOnLevel = message.Enable;
        }

        [WorldHandler(FriendJoinRequestMessage.Id)]
        public static void HandleFriendJoinRequestMessage(WorldClient client, FriendJoinRequestMessage message)
        {
            var character = World.Instance.GetCharacter(message.Name);

            client.Character.Teleport(character.Position);
        }

        public static void SendFriendWarnOnConnectionStateMessage(IPacketReceiver client, bool state)
        {
            client.Send(new FriendWarnOnConnectionStateMessage(state));
        }

        public static void SendFriendWarnOnLevelGainStateMessage(IPacketReceiver client, bool state)
        {
            client.Send(new FriendWarnOnLevelGainStateMessage(state));
        }

        public static void SendFriendAddFailureMessage(IPacketReceiver client, ListAddFailureEnum reason)
        {
            client.Send(new FriendAddFailureMessage((sbyte)reason));
        }

        public static void SendFriendAddedMessage(WorldClient client, Friend friend)
        {
            client.Send(new FriendAddedMessage(friend.GetFriendInformations(client.Character)));
        }

        public static void SendIgnoredAddedMessage(IPacketReceiver client, Ignored ignored, bool session)
        {
            client.Send(new IgnoredAddedMessage(ignored.GetIgnoredInformations(), session));
        }

        public static void SendFriendDeleteResultMessage(IPacketReceiver client, bool success, string name)
        {
            client.Send(new FriendDeleteResultMessage(success, name));
        }

        public static void SendFriendUpdateMessage(WorldClient client, Friend friend)
        {
            client.Send(new FriendUpdateMessage(friend.GetFriendInformations(client.Character)));
        }

        public static void SendFriendsListMessage(WorldClient client, IEnumerable<Friend> friends)
        {
            client.Send(new FriendsListMessage(friends.Select(entry => entry.GetFriendInformations(client.Character)).ToArray()));
        }

        public static void SendIgnoredAddFailureMessage(IPacketReceiver client,  ListAddFailureEnum reason)
        {
            client.Send(new IgnoredAddFailureMessage((sbyte)reason));
        }

        public static void SendIgnoredDeleteResultMessage(IPacketReceiver client, bool success, bool session, string name)
        {
            client.Send(new IgnoredDeleteResultMessage(success, session, name));
        }

        public static void SendIgnoredListMessage(IPacketReceiver client, IEnumerable<Ignored> ignoreds)
        {
            client.Send(new IgnoredListMessage(ignoreds.Where(x => !x.Session).Select(entry => entry.GetIgnoredInformations()).ToArray()));
        }

        public static void SendGuildListMessage(IPacketReceiver client)
        {
            client.Send(new GuildListMessage(GuildManager.Instance.GetCachedGuilds()));
        }

        public static void SendGuildVersatileInfoListMessage(IPacketReceiver client)
        {
            client.Send(new GuildVersatileInfoListMessage(GuildManager.Instance.GetCachedGuildsVersatile()));
        }
    }
}