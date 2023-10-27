namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class BidExchangerObjectInfo
    {
        public const short Id  = 122;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public uint ObjectUID { get; set; }
        public ObjectEffect[] Effects { get; set; }
        public ulong[] Prices { get; set; }

        public BidExchangerObjectInfo(uint objectUID, ObjectEffect[] effects, ulong[] prices)
        {
            this.ObjectUID = objectUID;
            this.Effects = effects;
            this.Prices = prices;
        }

        public BidExchangerObjectInfo() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ObjectUID);
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

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectUID = reader.ReadVarUInt();
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
