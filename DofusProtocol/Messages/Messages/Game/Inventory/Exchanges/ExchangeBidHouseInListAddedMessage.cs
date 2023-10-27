namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeBidHouseInListAddedMessage : Message
    {
        public const uint Id = 5949;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int ItemUID { get; set; }
        public int ObjGenericId { get; set; }
        public ObjectEffect[] Effects { get; set; }
        public ulong[] Prices { get; set; }

        public ExchangeBidHouseInListAddedMessage(int itemUID, int objGenericId, ObjectEffect[] effects, ulong[] prices)
        {
            this.ItemUID = itemUID;
            this.ObjGenericId = objGenericId;
            this.Effects = effects;
            this.Prices = prices;
        }

        public ExchangeBidHouseInListAddedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(ItemUID);
            writer.WriteInt(ObjGenericId);
            writer.WriteShort((short)Effects.Count());
            for (var effectsIndex = 0; effectsIndex < Effects.Count(); effectsIndex++)
            {
                var objectToSend = Effects[effectsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)Prices.Count());
            for (var pricesIndex = 0; pricesIndex < Prices.Count(); pricesIndex++)
            {
                writer.WriteVarULong(Prices[pricesIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            ItemUID = reader.ReadInt();
            ObjGenericId = reader.ReadInt();
            var effectsCount = reader.ReadUShort();
            Effects = new ObjectEffect[effectsCount];
            for (var effectsIndex = 0; effectsIndex < effectsCount; effectsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<ObjectEffect>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Effects[effectsIndex] = objectToAdd;
            }
            var pricesCount = reader.ReadUShort();
            Prices = new ulong[pricesCount];
            for (var pricesIndex = 0; pricesIndex < pricesCount; pricesIndex++)
            {
                Prices[pricesIndex] = reader.ReadVarULong();
            }
        }

    }
}
