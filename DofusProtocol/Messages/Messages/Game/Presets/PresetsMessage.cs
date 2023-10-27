namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PresetsMessage : Message
    {
        public const uint Id = 6750;
        public override uint MessageId
        {
            get { return Id; }
        }
        public Preset[] Presets { get; set; }

        public PresetsMessage(Preset[] presets)
        {
            this.Presets = presets;
        }

        public PresetsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
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
