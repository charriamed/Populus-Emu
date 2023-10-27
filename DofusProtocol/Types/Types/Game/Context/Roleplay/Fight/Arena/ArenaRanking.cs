namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ArenaRanking
    {
        public const short Id  = 554;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort Rank { get; set; }
        public ushort BestRank { get; set; }

        public ArenaRanking(ushort rank, ushort bestRank)
        {
            this.Rank = rank;
            this.BestRank = bestRank;
        }

        public ArenaRanking() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(Rank);
            writer.WriteVarUShort(BestRank);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            Rank = reader.ReadVarUShort();
            BestRank = reader.ReadVarUShort();
        }

    }
}
