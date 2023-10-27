namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyInvitationDetailsRequestMessage : AbstractPartyMessage
    {
        public new const uint Id = 6264;
        public override uint MessageId
        {
            get { return Id; }
        }

        public PartyInvitationDetailsRequestMessage(uint partyId)
        {
            this.PartyId = partyId;
        }

        public PartyInvitationDetailsRequestMessage() { }

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
