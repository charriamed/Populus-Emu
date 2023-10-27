namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PresetSaveRequestMessage : Message
    {
        public const uint Id = 6761;
        public override uint MessageId
        {
            get { return Id; }
        }
        public short PresetId { get; set; }
        public sbyte SymbolId { get; set; }
        public bool UpdateData { get; set; }

        public PresetSaveRequestMessage(short presetId, sbyte symbolId, bool updateData)
        {
            this.PresetId = presetId;
            this.SymbolId = symbolId;
            this.UpdateData = updateData;
        }

        public PresetSaveRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(PresetId);
            writer.WriteSByte(SymbolId);
            writer.WriteBoolean(UpdateData);
        }

        public override void Deserialize(IDataReader reader)
        {
            PresetId = reader.ReadShort();
            SymbolId = reader.ReadSByte();
            UpdateData = reader.ReadBoolean();
        }

    }
}
