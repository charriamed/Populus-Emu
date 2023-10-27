namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SequenceNumberMessage : Message
    {
        public const uint Id = 6317;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort Number { get; set; }

        public SequenceNumberMessage(ushort number)
        {
            this.Number = number;
        }

        public SequenceNumberMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUShort(Number);
        }

        public override void Deserialize(IDataReader reader)
        {
            Number = reader.ReadUShort();
        }

    }
}
