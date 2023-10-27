namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeShopStockMultiMovementRemovedMessage : Message
    {
        public const uint Id = 6037;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint[] ObjectIdList { get; set; }

        public ExchangeShopStockMultiMovementRemovedMessage(uint[] objectIdList)
        {
            this.ObjectIdList = objectIdList;
        }

        public ExchangeShopStockMultiMovementRemovedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)ObjectIdList.Count());
            for (var objectIdListIndex = 0; objectIdListIndex < ObjectIdList.Count(); objectIdListIndex++)
            {
                writer.WriteVarUInt(ObjectIdList[objectIdListIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var objectIdListCount = reader.ReadUShort();
            ObjectIdList = new uint[objectIdListCount];
            for (var objectIdListIndex = 0; objectIdListIndex < objectIdListCount; objectIdListIndex++)
            {
                ObjectIdList[objectIdListIndex] = reader.ReadVarUInt();
            }
        }

    }
}
