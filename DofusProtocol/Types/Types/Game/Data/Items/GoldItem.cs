namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GoldItem : Item
    {
        public new const short Id = 123;
        public override short TypeId
        {
            get { return Id; }
        }
        public ulong Sum { get; set; }

        public GoldItem(ulong sum)
        {
            this.Sum = sum;
        }

        public GoldItem() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(Sum);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Sum = reader.ReadVarULong();
        }

    }
}
