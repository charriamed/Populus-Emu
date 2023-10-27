namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectItemMinimalInformation : Item
    {
        public new const short Id = 124;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort ObjectGID { get; set; }
        public ObjectEffect[] Effects { get; set; }

        public ObjectItemMinimalInformation(ushort objectGID, ObjectEffect[] effects)
        {
            this.ObjectGID = objectGID;
            this.Effects = effects;
        }

        public ObjectItemMinimalInformation() { }

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
        }

    }
}
