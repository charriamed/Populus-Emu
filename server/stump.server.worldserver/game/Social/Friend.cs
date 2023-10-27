using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Accounts;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Social
{
    public class Friend
    {
        public Friend(AccountRelation relation, WorldAccount account)
        {
            Relation = relation;
            Account = account;
        }

        public Friend(AccountRelation relation, WorldAccount account, Character character)
        {
            Relation = relation;
            Account = account;
            Character = character;
        }

        public WorldAccount Account
        {
            get;
        }

        public Character Character
        {
            get;
            private set;
        }

        public AccountRelation Relation
        {
            get;
        }

        public void SetOnline(Character character)
        {
            if (character.Client.WorldAccount.Id != Account.Id)
                return;

            Character = character;
        }

        public void SetOffline()
        {
            Character = null;
        }

        public bool IsOnline() => Character != null;

        public FriendInformations GetFriendInformations(Character asker)
        {
            var hvb = HavenBags.HavenBagManager.Instance.GetHavenBagByOwner(Character);
            if (hvb == null && IsOnline())
            {
                return new FriendOnlineInformations(Account.Id,
                    Account.Nickname,
                    (sbyte)(Character.FriendsBook.IsFriend(asker.Account.Id) ? (Character.IsFighting() ? PlayerStateEnum.GAME_TYPE_FIGHT : PlayerStateEnum.GAME_TYPE_ROLEPLAY) : PlayerStateEnum.UNKNOWN_STATE),
                    (ushort)Account.LastConnectionTimeStamp,
                    0, // todo achievement
                    (short)Character.ArenaLeague.LeagueId,
                    0,
                    Character.Sex == SexTypeEnum.SEX_FEMALE,
                    false,
                    (ulong)Character.Id,
                    Character.Name,
                    Character.FriendsBook.IsFriend(asker.Account.Id) ? (byte)Character.Level : (byte)0,
                    Character.FriendsBook.IsFriend(asker.Account.Id) ? (sbyte)Character.AlignmentSide : (sbyte)AlignmentSideEnum.ALIGNMENT_UNKNOWN,
                    (sbyte)Character.Breed.Id,
                    Character.GuildMember == null ? new GuildInformations(0, "", 0, new GuildEmblem(0, 0, 0, 0)) : Character.GuildMember.Guild.GetGuildInformations(),
                    (ushort)Character.SmileyMoodId,
                    Character.Status);
            }
            else if (IsOnline())
            {
                return new FriendOnlineInformations(Account.Id,
                    Account.Nickname,
                    (sbyte)(Character.FriendsBook.IsFriend(asker.Account.Id) ? (Character.IsFighting() ? PlayerStateEnum.GAME_TYPE_FIGHT : PlayerStateEnum.GAME_TYPE_ROLEPLAY) : PlayerStateEnum.UNKNOWN_STATE),
                    (ushort)Account.LastConnectionTimeStamp,
                    0, // todo achievement
                    (short)Character.ArenaLeague.LeagueId,
                    0,
                    Character.Sex == SexTypeEnum.SEX_FEMALE,
                    hvb.FriendsAllowed,
                    (ulong)Character.Id,
                    Character.Name,
                    Character.FriendsBook.IsFriend(asker.Account.Id) ? (byte)Character.Level : (byte)0,
                    Character.FriendsBook.IsFriend(asker.Account.Id) ? (sbyte)Character.AlignmentSide : (sbyte)AlignmentSideEnum.ALIGNMENT_UNKNOWN,
                    (sbyte)Character.Breed.Id,
                    Character.GuildMember == null ? new GuildInformations(0, "", 0, new GuildEmblem(0, 0, 0, 0)) : Character.GuildMember.Guild.GetGuildInformations(),
                    (ushort)Character.SmileyMoodId,
                    Character.Status);
            }

            return new FriendInformations(
                Account.Id,
                Account.Nickname,
                (sbyte)PlayerStateEnum.NOT_CONNECTED,
                (ushort)Account.LastConnectionTimeStamp,
                0,
                0,
                0); // todo achievement
        }
    }
}