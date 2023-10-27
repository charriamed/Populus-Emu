namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyRefuseInvitationNotificationMessage : AbstractPartyEventMessage
    {
        public new const uint Id = 5596;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong GuestId { get; set; }

        public PartyRefuseInvitationNotificationMessage(uint partyId, ulong guestId)
        {
            this.PartyId = partyId;
            this.GuestId = guestId;
        }

        public PartyRefuseInvitationNotificationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(GuestId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            GuestId = reader.ReadVarULong();
        }

    }
}
