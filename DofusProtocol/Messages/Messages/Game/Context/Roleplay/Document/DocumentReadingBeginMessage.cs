namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class DocumentReadingBeginMessage : Message
    {
        public const uint Id = 5675;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort DocumentId { get; set; }

        public DocumentReadingBeginMessage(ushort documentId)
        {
            this.DocumentId = documentId;
        }

        public DocumentReadingBeginMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(DocumentId);
        }

        public override void Deserialize(IDataReader reader)
        {
            DocumentId = reader.ReadVarUShort();
        }

    }
}
