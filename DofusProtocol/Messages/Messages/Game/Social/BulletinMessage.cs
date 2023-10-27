namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BulletinMessage : SocialNoticeMessage
    {
        public new const uint Id = 6695;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int LastNotifiedTimestamp { get; set; }

        public BulletinMessage(string content, int timestamp, ulong memberId, string memberName, int lastNotifiedTimestamp)
        {
            this.Content = content;
            this.Timestamp = timestamp;
            this.MemberId = memberId;
            this.MemberName = memberName;
            this.LastNotifiedTimestamp = lastNotifiedTimestamp;
        }

        public BulletinMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(LastNotifiedTimestamp);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            LastNotifiedTimestamp = reader.ReadInt();
        }

    }
}
