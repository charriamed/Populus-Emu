namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyNewGuestMessage : AbstractPartyEventMessage
    {
        public new const uint Id = 6260;
        public override uint MessageId
        {
            get { return Id; }
        }
        public PartyGuestInformations Guest { get; set; }

        public PartyNewGuestMessage(uint partyId, PartyGuestInformations guest)
        {
            this.PartyId = partyId;
            this.Guest = guest;
        }

        public PartyNewGuestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            Guest.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Guest = new PartyGuestInformations();
            Guest.Deserialize(reader);
        }

    }
}
