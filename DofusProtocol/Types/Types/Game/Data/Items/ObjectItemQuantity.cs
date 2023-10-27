namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectItemQuantity : Item
    {
        public new const short Id = 119;
        public override short TypeId
        {
            get { return Id; }
        }
        public uint ObjectUID { get; set; }
        public uint Quantity { get; set; }

        public ObjectItemQuantity(uint objectUID, uint quantity)
        {
            this.ObjectUID = objectUID;
            this.Quantity = quantity;
        }

        public ObjectItemQuantity() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(ObjectUID);
            writer.WriteVarUInt(Quantity);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ObjectUID = reader.ReadVarUInt();
            Quantity = reader.ReadVarUInt();
        }

    }
}
