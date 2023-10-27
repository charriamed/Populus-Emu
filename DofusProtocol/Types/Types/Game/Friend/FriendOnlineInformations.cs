namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Enums;
    using Stump.Core.IO;

    [Serializable]
    public class FriendOnlineInformations : FriendInformations
    {
        public new const short Id = 92;
        public override short TypeId
        {
            get { return Id; }
        }
        public bool Sex { get; set; }
        public bool HavenBagShared { get; set; }
        public ulong PlayerId { get; set; }
        public string PlayerName { get; set; }
        public ushort Level { get; set; }
        public sbyte AlignmentSide { get; set; }
        public sbyte Breed { get; set; }
        public GuildInformations GuildInfo { get; set; }
        public ushort MoodSmileyId { get; set; }
        public PlayerStatus Status { get; set; }

        public FriendOnlineInformations(int accountId, string accountName, sbyte playerState, ushort lastConnection, int achievementPoints, short leagueId, int ladderPosition, bool sex, bool havenBagShared, ulong playerId, string playerName, ushort level, sbyte alignmentSide, sbyte breed, GuildInformations guildInfo, ushort moodSmileyId, PlayerStatus status)
        {
            this.AccountId = accountId;
            this.AccountName = accountName;
            this.PlayerState = playerState;
            this.LastConnection = lastConnection;
            this.AchievementPoints = achievementPoints;
            this.LeagueId = leagueId;
            this.LadderPosition = ladderPosition;
            this.Sex = sex;
            this.HavenBagShared = havenBagShared;
            this.PlayerId = playerId;
            this.PlayerName = playerName;
            this.Level = level;
            this.AlignmentSide = alignmentSide;
            this.Breed = breed;
            this.GuildInfo = guildInfo;
            this.MoodSmileyId = moodSmileyId;
            this.Status = status;
        }

        public FriendOnlineInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, Sex);
            flag = BooleanByteWrapper.SetFlag(flag, 1, HavenBagShared);
            writer.WriteByte(flag);
            writer.WriteVarULong(PlayerId);
            writer.WriteUTF(PlayerName);
            writer.WriteVarUShort(Level);
            writer.WriteSByte(AlignmentSide);
            writer.WriteSByte(Breed);
            GuildInfo.Serialize(writer);
            writer.WriteVarUShort(MoodSmileyId);
            writer.WriteShort(Status.TypeId);
            Status.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var flag = reader.ReadByte();
            Sex = BooleanByteWrapper.GetFlag(flag, 0);
            HavenBagShared = BooleanByteWrapper.GetFlag(flag, 1);
            PlayerId = reader.ReadVarULong();
            PlayerName = reader.ReadUTF();
            Level = reader.ReadVarUShort();
            AlignmentSide = reader.ReadSByte();
            Breed = reader.ReadSByte();
            GuildInfo = new GuildInformations();
            GuildInfo.Deserialize(reader);
            MoodSmileyId = reader.ReadVarUShort();
            Status = ProtocolTypeManager.GetInstance<PlayerStatus>(reader.ReadShort());
            Status.Deserialize(reader);
        }

    }
}
