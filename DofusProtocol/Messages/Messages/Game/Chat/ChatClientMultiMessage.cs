namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ChatClientMultiMessage : ChatAbstractClientMessage
    {
        public new const uint Id = 861;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Channel { get; set; }

        public ChatClientMultiMessage(string content, sbyte channel)
        {
            this.Content = content;
            this.Channel = channel;
        }

        public ChatClientMultiMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(Channel);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Channel = reader.ReadSByte();
        }

    }
}
