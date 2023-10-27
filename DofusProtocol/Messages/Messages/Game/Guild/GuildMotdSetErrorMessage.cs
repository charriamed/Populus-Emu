namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildMotdSetErrorMessage : SocialNoticeSetErrorMessage
    {
        public new const uint Id = 6591;
        public override uint MessageId
        {
            get { return Id; }
        }

        public GuildMotdSetErrorMessage(sbyte reason)
        {
            this.Reason = reason;
        }

        public GuildMotdSetErrorMessage() { }

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
