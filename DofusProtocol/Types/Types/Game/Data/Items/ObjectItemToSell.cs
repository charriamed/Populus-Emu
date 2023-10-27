namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectItemToSell : Item
    {
        public new const short Id = 120;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort ObjectGID { get; set; }
        public ObjectEffect[] Effects { get; set; }
        public uint ObjectUID { get; set; }
        public uint Quantity { get; set; }
        public ulong ObjectPrice { get; set; }

        public ObjectItemToSell(ushort objectGID, ObjectEffect[] effects, uint objectUID, uint quantity, ulong objectPrice)
        {
            this.ObjectGID = objectGID;
            this.Effects = effects;
            this.ObjectUID = objectUID;
            this.Quantity = quantity;
            this.ObjectPrice = objectPrice;
        }

        public ObjectItemToSell() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(ObjectGID);
            writer.WriteShort((short)Effects.Count());
            for (var effectsIndex = 0; effectsIndex < Effects.Count(); effectsIndex++)
            {
                var objectToSend = Effects[effectsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
            writer.WriteVarUInt(ObjectUID);
            writer.WriteVarUInt(Quantity);
            writer.WriteVarULong(ObjectPrice);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ObjectGID = reader.ReadVarUShort();
            var effectsCount = reader.ReadUShort();
            Effects = new ObjectEffect[effectsCount];
            for (var effectsIndex = 0; effectsIndex < effectsCount; effectsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<ObjectEffect>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Effects[effectsIndex] = objectToAdd;
            }
            ObjectUID = reader.ReadVarUInt();
            Quantity = reader.ReadVarUInt();
            ObjectPrice = reader.ReadVarULong();
        }

    }
}
