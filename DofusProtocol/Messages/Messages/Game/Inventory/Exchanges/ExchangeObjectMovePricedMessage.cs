namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeObjectMovePricedMessage : ExchangeObjectMoveMessage
    {
        public new const uint Id = 5514;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong Price { get; set; }

        public ExchangeObjectMovePricedMessage(uint objectUID, int quantity, ulong price)
        {
            this.ObjectUID = objectUID;
            this.Quantity = quantity;
            this.Price = price;
        }

        public ExchangeObjectMovePricedMessage() { }

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
