namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeBidHouseUnsoldItemsMessage : Message
    {
        public const uint Id = 6612;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectItemGenericQuantity[] Items { get; set; }

        public ExchangeBidHouseUnsoldItemsMessage(ObjectItemGenericQuantity[] items)
        {
            this.Items = items;
        }

        public ExchangeBidHouseUnsoldItemsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Items.Count());
            for (var itemsIndex = 0; itemsIndex < Items.Count(); itemsIndex++)
            {
                var objectToSend = Items[itemsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var itemsCount = reader.ReadUShort();
            Items = new ObjectItemGenericQuantity[itemsCount];
            for (var itemsIndex = 0; itemsIndex < itemsCount; itemsIndex++)
            {
                var objectToAdd = new ObjectItemGenericQuantity();
                objectToAdd.Deserialize(reader);
                Items[itemsIndex] = objectToAdd;
            }
        }

    }
}
