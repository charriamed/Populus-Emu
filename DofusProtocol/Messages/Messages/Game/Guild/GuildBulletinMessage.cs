namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildBulletinMessage : BulletinMessage
    {
        public new const uint Id = 6689;
        public override uint MessageId
        {
            get { return Id; }
        }

        public GuildBulletinMessage(string content, int timestamp, ulong memberId, string memberName, int lastNotifiedTimestamp)
        {
            this.Content = content;
            this.Timestamp = timestamp;
            this.MemberId = memberId;
            this.MemberName = memberName;
            this.LastNotifiedTimestamp = lastNotifiedTimestamp;
        }

        public GuildBulletinMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }

    }
}
