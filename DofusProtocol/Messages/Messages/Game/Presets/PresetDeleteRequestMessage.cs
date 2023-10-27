namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PresetDeleteRequestMessage : Message
    {
        public const uint Id = 6755;
        public override uint MessageId
        {
            get { return Id; }
        }
        public short PresetId { get; set; }

        public PresetDeleteRequestMessage(short presetId)
        {
            this.PresetId = presetId;
        }

        public PresetDeleteRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(PresetId);
        }

        public override void Deserialize(IDataReader reader)
        {
            PresetId = reader.ReadShort();
        }

    }
}
