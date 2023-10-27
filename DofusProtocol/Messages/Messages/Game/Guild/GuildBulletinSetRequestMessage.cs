namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildBulletinSetRequestMessage : SocialNoticeSetRequestMessage
    {
        public new const uint Id = 6694;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Content { get; set; }
        public bool NotifyMembers { get; set; }

        public GuildBulletinSetRequestMessage(string content, bool notifyMembers)
        {
            this.Content = content;
            this.NotifyMembers = notifyMembers;
        }

        public GuildBulletinSetRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(Content);
            writer.WriteBoolean(NotifyMembers);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Content = reader.ReadUTF();
            NotifyMembers = reader.ReadBoolean();
        }

    }
}
