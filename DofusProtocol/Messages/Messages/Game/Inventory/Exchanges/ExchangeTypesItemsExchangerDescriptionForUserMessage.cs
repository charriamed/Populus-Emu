namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeTypesItemsExchangerDescriptionForUserMessage : Message
    {
        public const uint Id = 5752;
        public override uint MessageId
        {
            get { return Id; }
        }
        public BidExchangerObjectInfo[] ItemTypeDescriptions { get; set; }

        public ExchangeTypesItemsExchangerDescriptionForUserMessage(BidExchangerObjectInfo[] itemTypeDescriptions)
        {
            this.ItemTypeDescriptions = itemTypeDescriptions;
        }

        public ExchangeTypesItemsExchangerDescriptionForUserMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)ItemTypeDescriptions.Count());
            for (var itemTypeDescriptionsIndex = 0; itemTypeDescriptionsIndex < ItemTypeDescriptions.Count(); itemTypeDescriptionsIndex++)
            {
                var objectToSend = ItemTypeDescriptions[itemTypeDescriptionsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var itemTypeDescriptionsCount = reader.ReadUShort();
            ItemTypeDescriptions = new BidExchangerObjectInfo[itemTypeDescriptionsCount];
            for (var itemTypeDescriptionsIndex = 0; itemTypeDescriptionsIndex < itemTypeDescriptionsCount; itemTypeDescriptionsIndex++)
            {
                var objectToAdd = new BidExchangerObjectInfo();
                objectToAdd.Deserialize(reader);
                ItemTypeDescriptions[itemTypeDescriptionsIndex] = objectToAdd;
            }
        }

    }
}
