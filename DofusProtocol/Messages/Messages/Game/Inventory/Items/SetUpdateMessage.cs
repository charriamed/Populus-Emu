namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class SetUpdateMessage : Message
    {
        public const uint Id = 5503;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort SetId { get; set; }
        public ushort[] SetObjects { get; set; }
        public ObjectEffect[] SetEffects { get; set; }

        public SetUpdateMessage(ushort setId, ushort[] setObjects, ObjectEffect[] setEffects)
        {
            this.SetId = setId;
            this.SetObjects = setObjects;
            this.SetEffects = setEffects;
        }

        public SetUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SetId);
            writer.WriteShort((short)SetObjects.Count());
            for (var setObjectsIndex = 0; setObjectsIndex < SetObjects.Count(); setObjectsIndex++)
            {
                writer.WriteVarUShort(SetObjects[setObjectsIndex]);
            }
            writer.WriteShort((short)SetEffects.Count());
            for (var setEffectsIndex = 0; setEffectsIndex < SetEffects.Count(); setEffectsIndex++)
            {
                var objectToSend = SetEffects[setEffectsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            SetId = reader.ReadVarUShort();
            var setObjectsCount = reader.ReadUShort();
            SetObjects = new ushort[setObjectsCount];
            for (var setObjectsIndex = 0; setObjectsIndex < setObjectsCount; setObjectsIndex++)
            {
                SetObjects[setObjectsIndex] = reader.ReadVarUShort();
            }
            var setEffectsCount = reader.ReadUShort();
            SetEffects = new ObjectEffect[setEffectsCount];
            for (var setEffectsIndex = 0; setEffectsIndex < setEffectsCount; setEffectsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<ObjectEffect>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                SetEffects[setEffectsIndex] = objectToAdd;
            }
        }

    }
}
