namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ComicReadingBeginMessage : Message
    {
        public const uint Id = 6536;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort ComicId { get; set; }

        public ComicReadingBeginMessage(ushort comicId)
        {
            this.ComicId = comicId;
        }

        public ComicReadingBeginMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ComicId);
        }

        public override void Deserialize(IDataReader reader)
        {
            ComicId = reader.ReadVarUShort();
        }

    }
}
