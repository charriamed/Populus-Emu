namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyInvitationCancelledForGuestMessage : AbstractPartyMessage
    {
        public new const uint Id = 6256;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong CancelerId { get; set; }

        public PartyInvitationCancelledForGuestMessage(uint partyId, ulong cancelerId)
        {
            this.PartyId = partyId;
            this.CancelerId = cancelerId;
        }

        public PartyInvitationCancelledForGuestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(CancelerId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            CancelerId = reader.ReadVarULong();
        }

    }
}
