namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SequenceEndMessage : Message
    {
        public const uint Id = 956;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort ActionId { get; set; }
        public double AuthorId { get; set; }
        public sbyte SequenceType { get; set; }

        public SequenceEndMessage(ushort actionId, double authorId, sbyte sequenceType)
        {
            this.ActionId = actionId;
            this.AuthorId = authorId;
            this.SequenceType = sequenceType;
        }

        public SequenceEndMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ActionId);
            writer.WriteDouble(AuthorId);
            writer.WriteSByte(SequenceType);
        }

        public override void Deserialize(IDataReader reader)
        {
            ActionId = reader.ReadVarUShort();
            AuthorId = reader.ReadDouble();
            SequenceType = reader.ReadSByte();
        }

    }
}
