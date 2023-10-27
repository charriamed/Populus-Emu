namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectItem : Item
    {
        public new const short Id = 37;
        public override short TypeId
        {
            get { return Id; }
        }
        public short Position { get; set; }
        public ushort ObjectGID { get; set; }
        public ObjectEffect[] Effects { get; set; }
        public uint ObjectUID { get; set; }
        public uint Quantity { get; set; }

        public ObjectItem(short position, ushort objectGID, ObjectEffect[] effects, uint objectUID, uint quantity)
        {
            this.Position = position;
            this.ObjectGID = objectGID;
            this.Effects = effects;
            this.ObjectUID = objectUID;
            this.Quantity = quantity;
        }

        public ObjectItem() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(Position);
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
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Position = reader.ReadShort();
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
        }

    }
}
