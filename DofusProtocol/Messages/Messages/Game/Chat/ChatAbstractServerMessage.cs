namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ChatAbstractServerMessage : Message
    {
        public const uint Id = 880;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Channel { get; set; }
        public string Content { get; set; }
        public int Timestamp { get; set; }
        public string Fingerprint { get; set; }

        public ChatAbstractServerMessage(sbyte channel, string content, int timestamp, string fingerprint)
        {
            this.Channel = channel;
            this.Content = content;
            this.Timestamp = timestamp;
            this.Fingerprint = fingerprint;
        }

        public ChatAbstractServerMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Channel);
            writer.WriteUTF(Content);
            writer.WriteInt(Timestamp);
            writer.WriteUTF(Fingerprint);
        }

        public override void Deserialize(IDataReader reader)
        {
            Channel = reader.ReadSByte();
            Content = reader.ReadUTF();
            Timestamp = reader.ReadInt();
            Fingerprint = reader.ReadUTF();
        }

    }
}
