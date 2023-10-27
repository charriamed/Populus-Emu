namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ArenaLeagueRanking
    {
        public const short Id  = 553;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort Rank { get; set; }
        public ushort LeagueId { get; set; }
        public short LeaguePoints { get; set; }
        public short TotalLeaguePoints { get; set; }
        public int LadderPosition { get; set; }

        public ArenaLeagueRanking(ushort rank, ushort leagueId, short leaguePoints, short totalLeaguePoints, int ladderPosition)
        {
            this.Rank = rank;
            this.LeagueId = leagueId;
            this.LeaguePoints = leaguePoints;
            this.TotalLeaguePoints = totalLeaguePoints;
            this.LadderPosition = ladderPosition;
        }

        public ArenaLeagueRanking() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(Rank);
            writer.WriteVarUShort(LeagueId);
            writer.WriteVarShort(LeaguePoints);
            writer.WriteVarShort(TotalLeaguePoints);
            writer.WriteInt(LadderPosition);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            Rank = reader.ReadVarUShort();
            LeagueId = reader.ReadVarUShort();
            LeaguePoints = reader.ReadVarShort();
            TotalLeaguePoints = reader.ReadVarShort();
            LadderPosition = reader.ReadInt();
        }

    }
}
