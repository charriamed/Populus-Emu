namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Enums;
    using Stump.Core.IO;

    [Serializable]
    public class LeagueFriendInformations : AbstractContactInformations
    {
        public new const short Id = 555;
        public override short TypeId
        {
            get { return Id; }
        }
        public ulong PlayerId { get; set; }
        public string PlayerName { get; set; }
        public sbyte Breed { get; set; }
        public bool Sex { get; set; }
        public ushort Level { get; set; }
        public short LeagueId { get; set; }
        public short TotalLeaguePoints { get; set; }
        public int LadderPosition { get; set; }

        public LeagueFriendInformations(int accountId, string accountName, ulong playerId, string playerName, sbyte breed, bool sex, ushort level, short leagueId, short totalLeaguePoints, int ladderPosition)
        {
            this.AccountId = accountId;
            this.AccountName = accountName;
            this.PlayerId = playerId;
            this.PlayerName = playerName;
            this.Breed = breed;
            this.Sex = sex;
            this.Level = level;
            this.LeagueId = leagueId;
            this.TotalLeaguePoints = totalLeaguePoints;
            this.LadderPosition = ladderPosition;
        }

        public LeagueFriendInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(PlayerId);
            writer.WriteUTF(PlayerName);
            writer.WriteSByte(Breed);
            writer.WriteBoolean(Sex);
            writer.WriteVarUShort(Level);
            writer.WriteVarShort(LeagueId);
            writer.WriteVarShort(TotalLeaguePoints);
            writer.WriteInt(LadderPosition);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PlayerId = reader.ReadVarULong();
            PlayerName = reader.ReadUTF();
            Breed = reader.ReadSByte();
            Sex = reader.ReadBoolean();
            Level = reader.ReadVarUShort();
            LeagueId = reader.ReadVarShort();
            TotalLeaguePoints = reader.ReadVarShort();
            LadderPosition = reader.ReadInt();
        }

    }
}
