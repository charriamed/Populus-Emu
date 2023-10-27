namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeStartOkHumanVendorMessage : Message
    {
        public const uint Id = 5767;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double SellerId { get; set; }
        public ObjectItemToSellInHumanVendorShop[] ObjectsInfos { get; set; }

        public ExchangeStartOkHumanVendorMessage(double sellerId, ObjectItemToSellInHumanVendorShop[] objectsInfos)
        {
            this.SellerId = sellerId;
            this.ObjectsInfos = objectsInfos;
        }

        public ExchangeStartOkHumanVendorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(SellerId);
            writer.WriteShort((short)ObjectsInfos.Count());
            for (var objectsInfosIndex = 0; objectsInfosIndex < ObjectsInfos.Count(); objectsInfosIndex++)
            {
                var objectToSend = ObjectsInfos[objectsInfosIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            SellerId = reader.ReadDouble();
            var objectsInfosCount = reader.ReadUShort();
            ObjectsInfos = new ObjectItemToSellInHumanVendorShop[objectsInfosCount];
            for (var objectsInfosIndex = 0; objectsInfosIndex < objectsInfosCount; objectsInfosIndex++)
            {
                var objectToAdd = new ObjectItemToSellInHumanVendorShop();
                objectToAdd.Deserialize(reader);
                ObjectsInfos[objectsInfosIndex] = objectToAdd;
            }
        }

    }
}
