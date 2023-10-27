namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PopupWarningMessage : Message
    {
        public const uint Id = 6134;
        public override uint MessageId
        {
            get { return Id; }
        }
        public byte LockDuration { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }

        public PopupWarningMessage(byte lockDuration, string author, string content)
        {
            this.LockDuration = lockDuration;
            this.Author = author;
            this.Content = content;
        }

        public PopupWarningMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(LockDuration);
            writer.WriteUTF(Author);
            writer.WriteUTF(Content);
        }

        public override void Deserialize(IDataReader reader)
        {
            LockDuration = reader.ReadByte();
            Author = reader.ReadUTF();
            Content = reader.ReadUTF();
        }

    }
}
