namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyAcceptInvitationMessage : AbstractPartyMessage
    {
        public new const uint Id = 5580;
        public override uint MessageId
        {
            get { return Id; }
        }

        public PartyAcceptInvitationMessage(uint partyId)
        {
            this.PartyId = partyId;
        }

        public PartyAcceptInvitationMessage() { }

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
