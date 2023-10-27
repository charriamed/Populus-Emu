namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectItemGenericQuantityPrice : ObjectItemGenericQuantity
    {
        public new const short Id = 494;
        public override short TypeId
        {
            get { return Id; }
        }
        public ulong Price { get; set; }

        public ObjectItemGenericQuantityPrice(ushort objectGID, uint quantity, ulong price)
        {
            this.ObjectGID = objectGID;
            this.Quantity = quantity;
            this.Price = price;
        }

        public ObjectItemGenericQuantityPrice() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(Price);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Price = reader.ReadVarULong();
        }

    }
}
