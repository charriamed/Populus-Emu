namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeStartOkNpcShopMessage : Message
    {
        public const uint Id = 5761;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double NpcSellerId { get; set; }
        public ushort TokenId { get; set; }
        public ObjectItemToSellInNpcShop[] ObjectsInfos { get; set; }

        public ExchangeStartOkNpcShopMessage(double npcSellerId, ushort tokenId, ObjectItemToSellInNpcShop[] objectsInfos)
        {
            this.NpcSellerId = npcSellerId;
            this.TokenId = tokenId;
            this.ObjectsInfos = objectsInfos;
        }

        public ExchangeStartOkNpcShopMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(NpcSellerId);
            writer.WriteVarUShort(TokenId);
            writer.WriteShort((short)ObjectsInfos.Count());
            for (var objectsInfosIndex = 0; objectsInfosIndex < ObjectsInfos.Count(); objectsInfosIndex++)
            {
                var objectToSend = ObjectsInfos[objectsInfosIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            NpcSellerId = reader.ReadDouble();
            TokenId = reader.ReadVarUShort();
            var objectsInfosCount = reader.ReadUShort();
            ObjectsInfos = new ObjectItemToSellInNpcShop[objectsInfosCount];
            for (var objectsInfosIndex = 0; objectsInfosIndex < objectsInfosCount; objectsInfosIndex++)
            {
                var objectToAdd = new ObjectItemToSellInNpcShop();
                objectToAdd.Deserialize(reader);
                ObjectsInfos[objectsInfosIndex] = objectToAdd;
            }
        }

    }
}
