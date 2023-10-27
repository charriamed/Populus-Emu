namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PresetsContainerPreset : Preset
    {
        public new const short Id = 520;
        public override short TypeId
        {
            get { return Id; }
        }
        public Preset[] Presets { get; set; }

        public PresetsContainerPreset(short objectId, Preset[] presets)
        {
            this.ObjectId = objectId;
            this.Presets = presets;
        }

        public PresetsContainerPreset() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)Presets.Count());
            for (var presetsIndex = 0; presetsIndex < Presets.Count(); presetsIndex++)
            {
                var objectToSend = Presets[presetsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var presetsCount = reader.ReadUShort();
            Presets = new Preset[presetsCount];
            for (var presetsIndex = 0; presetsIndex < presetsCount; presetsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<Preset>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Presets[presetsIndex] = objectToAdd;
            }
        }

    }
}
