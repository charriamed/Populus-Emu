namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PresetDeleteResultMessage : Message
    {
        public const uint Id = 6748;
        public override uint MessageId
        {
            get { return Id; }
        }
        public short PresetId { get; set; }
        public sbyte Code { get; set; }

        public PresetDeleteResultMessage(short presetId, sbyte code)
        {
            this.PresetId = presetId;
            this.Code = code;
        }

        public PresetDeleteResultMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(PresetId);
            writer.WriteSByte(Code);
        }

        public override void Deserialize(IDataReader reader)
        {
            PresetId = reader.ReadShort();
            Code = reader.ReadSByte();
        }

    }
}
