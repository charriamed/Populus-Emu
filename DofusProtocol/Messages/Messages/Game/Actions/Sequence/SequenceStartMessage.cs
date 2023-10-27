namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SequenceStartMessage : Message
    {
        public const uint Id = 955;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte SequenceType { get; set; }
        public double AuthorId { get; set; }

        public SequenceStartMessage(sbyte sequenceType, double authorId)
        {
            this.SequenceType = sequenceType;
            this.AuthorId = authorId;
        }

        public SequenceStartMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(SequenceType);
            writer.WriteDouble(AuthorId);
        }

        public override void Deserialize(IDataReader reader)
        {
            SequenceType = reader.ReadSByte();
            AuthorId = reader.ReadDouble();
        }

    }
}
