namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ChatClientPrivateMessage : ChatAbstractClientMessage
    {
        public new const uint Id = 851;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Receiver { get; set; }

        public ChatClientPrivateMessage(string content, string receiver)
        {
            this.Content = content;
            this.Receiver = receiver;
        }

        public ChatClientPrivateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(Receiver);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Receiver = reader.ReadUTF();
        }

    }
}
