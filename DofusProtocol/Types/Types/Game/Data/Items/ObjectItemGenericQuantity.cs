namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectItemGenericQuantity : Item
    {
        public new const short Id = 483;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort ObjectGID { get; set; }
        public uint Quantity { get; set; }

        public ObjectItemGenericQuantity(ushort objectGID, uint quantity)
        {
            this.ObjectGID = objectGID;
            this.Quantity = quantity;
        }

        public ObjectItemGenericQuantity() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(ObjectGID);
            writer.WriteVarUInt(Quantity);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ObjectGID = reader.ReadVarUShort();
            Quantity = reader.ReadVarUInt();
        }

    }
}
