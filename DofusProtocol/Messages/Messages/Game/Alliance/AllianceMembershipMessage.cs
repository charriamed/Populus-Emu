namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceMembershipMessage : AllianceJoinedMessage
    {
        public new const uint Id = 6390;
        public override uint MessageId
        {
            get { return Id; }
        }

        public AllianceMembershipMessage(AllianceInformations allianceInfo, bool enabled, uint leadingGuildId)
        {
            this.AllianceInfo = allianceInfo;
            this.Enabled = enabled;
            this.LeadingGuildId = leadingGuildId;
        }

        public AllianceMembershipMessage() { }

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
