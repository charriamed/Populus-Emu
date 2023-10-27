namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ShortcutEntitiesPreset : Shortcut
    {
        public new const short Id = 544;
        public override short TypeId
        {
            get { return Id; }
        }
        public short PresetId { get; set; }

        public ShortcutEntitiesPreset(sbyte slot, short presetId)
        {
            this.Slot = slot;
            this.PresetId = presetId;
        }

        public ShortcutEntitiesPreset() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(PresetId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PresetId = reader.ReadShort();
        }

    }
}
