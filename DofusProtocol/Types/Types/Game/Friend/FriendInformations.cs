namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FriendInformations : AbstractContactInformations
    {
        public new const short Id = 78;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte PlayerState { get; set; }
        public ushort LastConnection { get; set; }
        public int AchievementPoints { get; set; }
        public short LeagueId { get; set; }
        public int LadderPosition { get; set; }

        public FriendInformations(int accountId, string accountName, sbyte playerState, ushort lastConnection, int achievementPoints, short leagueId, int ladderPosition)
        {
            this.AccountId = accountId;
            this.AccountName = accountName;
            this.PlayerState = playerState;
            this.LastConnection = lastConnection;
            this.AchievementPoints = achievementPoints;
            this.LeagueId = leagueId;
            this.LadderPosition = ladderPosition;
        }

        public FriendInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(PlayerState);
            writer.WriteVarUShort(LastConnection);
            writer.WriteInt(AchievementPoints);
            writer.WriteVarShort(LeagueId);
            writer.WriteInt(LadderPosition);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PlayerState = reader.ReadSByte();
            LastConnection = reader.ReadVarUShort();
            AchievementPoints = reader.ReadInt();
            LeagueId = reader.ReadVarShort();
            LadderPosition = reader.ReadInt();
        }

    }
}
