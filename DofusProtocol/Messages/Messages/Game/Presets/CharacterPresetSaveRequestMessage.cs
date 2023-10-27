namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterPresetSaveRequestMessage : PresetSaveRequestMessage
    {
        public new const uint Id = 6756;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Name { get; set; }

        public CharacterPresetSaveRequestMessage(short presetId, sbyte symbolId, bool updateData, string name)
        {
            this.PresetId = presetId;
            this.SymbolId = symbolId;
            this.UpdateData = updateData;
            this.Name = name;
        }

        public CharacterPresetSaveRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(Name);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Name = reader.ReadUTF();
        }

    }
}
