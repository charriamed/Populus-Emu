namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ItemForPresetUpdateMessage : Message
    {
        public const uint Id = 6760;
        public override uint MessageId
        {
            get { return Id; }
        }
        public short PresetId { get; set; }
        public ItemForPreset PresetItem { get; set; }

        public ItemForPresetUpdateMessage(short presetId, ItemForPreset presetItem)
        {
            this.PresetId = presetId;
            this.PresetItem = presetItem;
        }

        public ItemForPresetUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(PresetId);
            PresetItem.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            PresetId = reader.ReadShort();
            PresetItem = new ItemForPreset();
            PresetItem.Deserialize(reader);
        }

    }
}
