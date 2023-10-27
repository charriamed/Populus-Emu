namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeOfflineSoldItemsMessage : Message
    {
        public const uint Id = 6613;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectItemGenericQuantityPrice[] BidHouseItems { get; set; }
        public ObjectItemGenericQuantityPrice[] MerchantItems { get; set; }

        public ExchangeOfflineSoldItemsMessage(ObjectItemGenericQuantityPrice[] bidHouseItems, ObjectItemGenericQuantityPrice[] merchantItems)
        {
            this.BidHouseItems = bidHouseItems;
            this.MerchantItems = merchantItems;
        }

        public ExchangeOfflineSoldItemsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)BidHouseItems.Count());
            for (var bidHouseItemsIndex = 0; bidHouseItemsIndex < BidHouseItems.Count(); bidHouseItemsIndex++)
            {
                var objectToSend = BidHouseItems[bidHouseItemsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)MerchantItems.Count());
            for (var merchantItemsIndex = 0; merchantItemsIndex < MerchantItems.Count(); merchantItemsIndex++)
            {
                var objectToSend = MerchantItems[merchantItemsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var bidHouseItemsCount = reader.ReadUShort();
            BidHouseItems = new ObjectItemGenericQuantityPrice[bidHouseItemsCount];
            for (var bidHouseItemsIndex = 0; bidHouseItemsIndex < bidHouseItemsCount; bidHouseItemsIndex++)
            {
                var objectToAdd = new ObjectItemGenericQuantityPrice();
                objectToAdd.Deserialize(reader);
                BidHouseItems[bidHouseItemsIndex] = objectToAdd;
            }
            var merchantItemsCount = reader.ReadUShort();
            MerchantItems = new ObjectItemGenericQuantityPrice[merchantItemsCount];
            for (var merchantItemsIndex = 0; merchantItemsIndex < merchantItemsCount; merchantItemsIndex++)
            {
                var objectToAdd = new ObjectItemGenericQuantityPrice();
                objectToAdd.Deserialize(reader);
                MerchantItems[merchantItemsIndex] = objectToAdd;
            }
        }

    }
}
