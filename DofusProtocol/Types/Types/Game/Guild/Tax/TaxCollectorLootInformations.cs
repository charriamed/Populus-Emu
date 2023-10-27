namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TaxCollectorLootInformations : TaxCollectorComplementaryInformations
    {
        public new const short Id = 372;
        public override short TypeId
        {
            get { return Id; }
        }
        public ulong Kamas { get; set; }
        public ulong Experience { get; set; }
        public uint Pods { get; set; }
        public ulong ItemsValue { get; set; }

        public TaxCollectorLootInformations(ulong kamas, ulong experience, uint pods, ulong itemsValue)
        {
            this.Kamas = kamas;
            this.Experience = experience;
            this.Pods = pods;
            this.ItemsValue = itemsValue;
        }

        public TaxCollectorLootInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(Kamas);
            writer.WriteVarULong(Experience);
            writer.WriteVarUInt(Pods);
            writer.WriteVarULong(ItemsValue);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Kamas = reader.ReadVarULong();
            Experience = reader.ReadVarULong();
            Pods = reader.ReadVarUInt();
            ItemsValue = reader.ReadVarULong();
        }

    }
}
