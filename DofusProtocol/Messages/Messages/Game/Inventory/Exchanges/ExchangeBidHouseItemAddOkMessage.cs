namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeBidHouseItemAddOkMessage : Message
    {
        public const uint Id = 5945;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectItemToSellInBid ItemInfo { get; set; }

        public ExchangeBidHouseItemAddOkMessage(ObjectItemToSellInBid itemInfo)
        {
            this.ItemInfo = itemInfo;
        }

        public ExchangeBidHouseItemAddOkMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            ItemInfo.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            ItemInfo = new ObjectItemToSellInBid();
            ItemInfo.Deserialize(reader);
        }

    }
}
