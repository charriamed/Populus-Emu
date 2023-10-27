namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeBidHouseBuyMessage : Message
    {
        public const uint Id = 5804;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint Uid { get; set; }
        public uint Qty { get; set; }
        public ulong Price { get; set; }

        public ExchangeBidHouseBuyMessage(uint uid, uint qty, ulong price)
        {
            this.Uid = uid;
            this.Qty = qty;
            this.Price = price;
        }

        public ExchangeBidHouseBuyMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(Uid);
            writer.WriteVarUInt(Qty);
            writer.WriteVarULong(Price);
        }

        public override void Deserialize(IDataReader reader)
        {
            Uid = reader.ReadVarUInt();
            Qty = reader.ReadVarUInt();
            Price = reader.ReadVarULong();
        }

    }
}
