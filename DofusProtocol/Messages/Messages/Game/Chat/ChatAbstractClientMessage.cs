namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ChatAbstractClientMessage : Message
    {
        public const uint Id = 850;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Content { get; set; }

        public ChatAbstractClientMessage(string content)
        {
            this.Content = content;
        }

        public ChatAbstractClientMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Content);
        }

        public override void Deserialize(IDataReader reader)
        {
            Content = reader.ReadUTF();
        }

    }
}
