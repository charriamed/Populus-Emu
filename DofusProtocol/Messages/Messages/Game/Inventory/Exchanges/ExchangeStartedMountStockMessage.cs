namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeStartedMountStockMessage : Message
    {
        public const uint Id = 5984;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectItem[] ObjectsInfos { get; set; }

        public ExchangeStartedMountStockMessage(ObjectItem[] objectsInfos)
        {
            this.ObjectsInfos = objectsInfos;
        }

        public ExchangeStartedMountStockMessage() { }

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
            ObjectsInfos = new ObjectItem[objectsInfosCount];
            for (var objectsInfosIndex = 0; objectsInfosIndex < objectsInfosCount; objectsInfosIndex++)
            {
                var objectToAdd = new ObjectItem();
                objectToAdd.Deserialize(reader);
                ObjectsInfos[objectsInfosIndex] = objectToAdd;
            }
        }

    }
}
