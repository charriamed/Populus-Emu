namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SocialNoticeMessage : Message
    {
        public const uint Id = 6688;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Content { get; set; }
        public int Timestamp { get; set; }
        public ulong MemberId { get; set; }
        public string MemberName { get; set; }

        public SocialNoticeMessage(string content, int timestamp, ulong memberId, string memberName)
        {
            this.Content = content;
            this.Timestamp = timestamp;
            this.MemberId = memberId;
            this.MemberName = memberName;
        }

        public SocialNoticeMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Content);
            writer.WriteInt(Timestamp);
            writer.WriteVarULong(MemberId);
            writer.WriteUTF(MemberName);
        }

        public override void Deserialize(IDataReader reader)
        {
            Content = reader.ReadUTF();
            Timestamp = reader.ReadInt();
            MemberId = reader.ReadVarULong();
            MemberName = reader.ReadUTF();
        }

    }
}
