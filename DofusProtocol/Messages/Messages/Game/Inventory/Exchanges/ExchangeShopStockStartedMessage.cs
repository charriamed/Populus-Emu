namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeShopStockStartedMessage : Message
    {
        public const uint Id = 5910;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectItemToSell[] ObjectsInfos { get; set; }

        public ExchangeShopStockStartedMessage(ObjectItemToSell[] objectsInfos)
        {
            this.ObjectsInfos = objectsInfos;
        }

        public ExchangeShopStockStartedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)ObjectsInfos.Count());
            for (var objectsInfosIndex = 0; objectsInfosIndex < ObjectsInfos.Count(); objectsInfosIndex++)
            {
                var objectToSend = ObjectsInfos[objectsInfosIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var objectsInfosCount = reader.ReadUShort();
            ObjectsInfos = new ObjectItemToSell[objectsInfosCount];
            for (var objectsInfosIndex = 0; objectsInfosIndex < objectsInfosCount; objectsInfosIndex++)
            {
                var objectToAdd = new ObjectItemToSell();
                objectToAdd.Deserialize(reader);
                ObjectsInfos[objectsInfosIndex] = objectToAdd;
            }
        }

    }
}
