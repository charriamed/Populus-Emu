namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeShopStockMultiMovementUpdatedMessage : Message
    {
        public const uint Id = 6038;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectItemToSell[] ObjectInfoList { get; set; }

        public ExchangeShopStockMultiMovementUpdatedMessage(ObjectItemToSell[] objectInfoList)
        {
            this.ObjectInfoList = objectInfoList;
        }

        public ExchangeShopStockMultiMovementUpdatedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)ObjectInfoList.Count());
            for (var objectInfoListIndex = 0; objectInfoListIndex < ObjectInfoList.Count(); objectInfoListIndex++)
            {
                var objectToSend = ObjectInfoList[objectInfoListIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var objectInfoListCount = reader.ReadUShort();
            ObjectInfoList = new ObjectItemToSell[objectInfoListCount];
            for (var objectInfoListIndex = 0; objectInfoListIndex < objectInfoListCount; objectInfoListIndex++)
            {
                var objectToAdd = new ObjectItemToSell();
                objectToAdd.Deserialize(reader);
                ObjectInfoList[objectInfoListIndex] = objectToAdd;
            }
        }

    }
}
