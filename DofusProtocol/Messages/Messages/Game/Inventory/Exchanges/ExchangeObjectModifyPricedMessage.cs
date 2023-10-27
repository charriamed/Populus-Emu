namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeObjectModifyPricedMessage : ExchangeObjectMovePricedMessage
    {
        public new const uint Id = 6238;
        public override uint MessageId
        {
            get { return Id; }
        }

        public ExchangeObjectModifyPricedMessage(uint objectUID, int quantity, ulong price)
        {
            this.ObjectUID = objectUID;
            this.Quantity = quantity;
            this.Price = price;
        }

        public ExchangeObjectModifyPricedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }

    }
}
