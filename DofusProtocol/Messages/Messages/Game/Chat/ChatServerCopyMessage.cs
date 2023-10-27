namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ChatServerCopyMessage : ChatAbstractServerMessage
    {
        public new const uint Id = 882;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong ReceiverId { get; set; }
        public string ReceiverName { get; set; }

        public ChatServerCopyMessage(sbyte channel, string content, int timestamp, string fingerprint, ulong receiverId, string receiverName)
        {
            this.Channel = channel;
            this.Content = content;
            this.Timestamp = timestamp;
            this.Fingerprint = fingerprint;
            this.ReceiverId = receiverId;
            this.ReceiverName = receiverName;
        }

        public ChatServerCopyMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(ReceiverId);
            writer.WriteUTF(ReceiverName);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ReceiverId = reader.ReadVarULong();
            ReceiverName = reader.ReadUTF();
        }

    }
}
