namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyRefuseInvitationMessage : AbstractPartyMessage
    {
        public new const uint Id = 5582;
        public override uint MessageId
        {
            get { return Id; }
        }

        public PartyRefuseInvitationMessage(uint partyId)
        {
            this.PartyId = partyId;
        }

        public PartyRefuseInvitationMessage() { }

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
