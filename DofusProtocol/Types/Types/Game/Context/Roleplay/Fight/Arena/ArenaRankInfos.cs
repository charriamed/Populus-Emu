namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ArenaRankInfos
    {
        public const short Id = 499;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ArenaRanking ranking;
        public ArenaLeagueRanking leagueRanking;
        public ushort victoryCount;
        public ushort fightcount;
        public short numFightNeededForLadder;

        public ArenaRankInfos(ArenaRanking ranking, ArenaLeagueRanking leagueRanking, ushort victoryCount, ushort fightcount, short numFightNeededForLadder)
        {
            this.ranking = ranking;
            this.leagueRanking = leagueRanking;
            this.victoryCount = victoryCount;
            this.fightcount = fightcount;
            this.numFightNeededForLadder = numFightNeededForLadder;
        }

        public ArenaRankInfos() { }

        public virtual void Serialize(IDataWriter writer)
        {
            if (ranking == null)
                writer.WriteByte(0);
            else
            {
                writer.WriteByte(1);
                ranking.Serialize(writer);
            }

            if (leagueRanking == null)
            {
                writer.WriteByte(0);
            }
            else
            {
                writer.WriteByte(1);
                leagueRanking.Serialize(writer);
            }

            writer.WriteVarUShort(victoryCount);
            writer.WriteVarUShort(fightcount);
            writer.WriteShort(numFightNeededForLadder);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            var isRankingAvailable = reader.ReadByte();
            if (isRankingAvailable == 1)
            {
                ranking = new ArenaRanking();
                ranking.Deserialize(reader);
            }

            var isLeagueRankingAvailable = reader.ReadByte();
            if (isLeagueRankingAvailable == 1)
            {
                leagueRanking = new ArenaLeagueRanking();
                leagueRanking.Deserialize(reader);
            }
            victoryCount = reader.ReadVarUShort();
            fightcount = reader.ReadVarUShort();
            numFightNeededForLadder = reader.ReadShort();
        }
    }
}
