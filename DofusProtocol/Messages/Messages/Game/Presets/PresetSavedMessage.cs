namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PresetSavedMessage : Message
    {
        public const uint Id = 6763;
        public override uint MessageId
        {
            get { return Id; }
        }
        public short PresetId { get; set; }
        public Preset Preset { get; set; }

        public PresetSavedMessage(short presetId, Preset preset)
        {
            this.PresetId = presetId;
            this.Preset = preset;
        }

        public PresetSavedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(PresetId);
            writer.WriteShort(Preset.TypeId);
            Preset.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            PresetId = reader.ReadShort();
            Preset = ProtocolTypeManager.GetInstance<Preset>(reader.ReadShort());
            Preset.Deserialize(reader);
        }

    }
}
