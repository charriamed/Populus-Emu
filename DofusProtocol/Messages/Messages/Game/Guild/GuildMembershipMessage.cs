namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildMembershipMessage : GuildJoinedMessage
    {
        public new const uint Id = 5835;
        public override uint MessageId
        {
            get { return Id; }
        }

        public GuildMembershipMessage(GuildInformations guildInfo, uint memberRights)
        {
            this.GuildInfo = guildInfo;
            this.MemberRights = memberRights;
        }

        public GuildMembershipMessage() { }

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
