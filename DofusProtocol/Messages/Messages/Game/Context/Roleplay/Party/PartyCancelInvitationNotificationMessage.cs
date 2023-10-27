namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyCancelInvitationNotificationMessage : AbstractPartyEventMessage
    {
        public new const uint Id = 6251;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong CancelerId { get; set; }
        public ulong GuestId { get; set; }

        public PartyCancelInvitationNotificationMessage(uint partyId, ulong cancelerId, ulong guestId)
        {
            this.PartyId = partyId;
            this.CancelerId = cancelerId;
            this.GuestId = guestId;
        }

        public PartyCancelInvitationNotificationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(CancelerId);
            writer.WriteVarULong(GuestId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            CancelerId = reader.ReadVarULong();
            GuestId = reader.ReadVarULong();
        }

    }
}
