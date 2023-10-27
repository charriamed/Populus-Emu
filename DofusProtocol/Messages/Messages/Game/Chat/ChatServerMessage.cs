namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ChatServerMessage : ChatAbstractServerMessage
    {
        public new const uint Id = 881;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double SenderId { get; set; }
        public string SenderName { get; set; }
        public string Prefix { get; set; }
        public int SenderAccountId { get; set; }

        public ChatServerMessage(sbyte channel, string content, int timestamp, string fingerprint, double senderId, string senderName, string prefix, int senderAccountId)
        {
            this.Channel = channel;
            this.Content = content;
            this.Timestamp = timestamp;
            this.Fingerprint = fingerprint;
            this.SenderId = senderId;
            this.SenderName = senderName;
            this.Prefix = prefix;
            this.SenderAccountId = senderAccountId;
        }

        public ChatServerMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(SenderId);
            writer.WriteUTF(SenderName);
            writer.WriteUTF(Prefix);
            writer.WriteInt(SenderAccountId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            SenderId = reader.ReadDouble();
            SenderName = reader.ReadUTF();
            Prefix = reader.ReadUTF();
            SenderAccountId = reader.ReadInt();
        }

    }
}
