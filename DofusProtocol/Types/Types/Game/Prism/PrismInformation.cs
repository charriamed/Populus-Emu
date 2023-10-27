namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PrismInformation
    {
        public const short Id  = 428;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte @typeId { get; set; }
        public sbyte State { get; set; }
        public int NextVulnerabilityDate { get; set; }
        public int PlacementDate { get; set; }
        public uint RewardTokenCount { get; set; }

        public PrismInformation(sbyte @typeId, sbyte state, int nextVulnerabilityDate, int placementDate, uint rewardTokenCount)
        {
            this.@typeId = @typeId;
            this.State = state;
            this.NextVulnerabilityDate = nextVulnerabilityDate;
            this.PlacementDate = placementDate;
            this.RewardTokenCount = rewardTokenCount;
        }

        public PrismInformation() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(@typeId);
            writer.WriteSByte(State);
            writer.WriteInt(NextVulnerabilityDate);
            writer.WriteInt(PlacementDate);
            writer.WriteVarUInt(RewardTokenCount);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            @typeId = reader.ReadSByte();
            State = reader.ReadSByte();
            NextVulnerabilityDate = reader.ReadInt();
            PlacementDate = reader.ReadInt();
            RewardTokenCount = reader.ReadVarUInt();
        }

    }
}
