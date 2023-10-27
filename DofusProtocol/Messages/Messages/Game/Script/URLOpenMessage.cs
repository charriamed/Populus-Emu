namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class URLOpenMessage : Message
    {
        public const uint Id = 6266;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte UrlId { get; set; }

        public URLOpenMessage(sbyte urlId)
        {
            this.UrlId = urlId;
        }

        public URLOpenMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(UrlId);
        }

        public override void Deserialize(IDataReader reader)
        {
            UrlId = reader.ReadSByte();
        }

    }
}
