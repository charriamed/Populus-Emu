namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterBuildPreset : PresetsContainerPreset
    {
        public new const short Id = 534;
        public override short TypeId
        {
            get { return Id; }
        }
        public short IconId { get; set; }
        public string Name { get; set; }

        public CharacterBuildPreset(short objectId, Preset[] presets, short iconId, string name)
        {
            this.ObjectId = objectId;
            this.Presets = presets;
            this.IconId = iconId;
            this.Name = name;
        }

        public CharacterBuildPreset() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(IconId);
            writer.WriteUTF(Name);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            IconId = reader.ReadShort();
            Name = reader.ReadUTF();
        }

    }
}
