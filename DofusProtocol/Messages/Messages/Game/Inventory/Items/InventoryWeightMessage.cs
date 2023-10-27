namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class InventoryWeightMessage : Message
    {
        public const uint Id = 3009;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint Weight { get; set; }
        public uint WeightMax { get; set; }

        public InventoryWeightMessage(uint weight, uint weightMax)
        {
            this.Weight = weight;
            this.WeightMax = weightMax;
        }

        public InventoryWeightMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(Weight);
            writer.WriteVarUInt(WeightMax);
        }

        public override void Deserialize(IDataReader reader)
        {
            Weight = reader.ReadVarUInt();
            WeightMax = reader.ReadVarUInt();
        }

    }
}
