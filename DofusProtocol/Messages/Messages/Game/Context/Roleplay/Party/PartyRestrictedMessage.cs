namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyRestrictedMessage : AbstractPartyMessage
    {
        public new const uint Id = 6175;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Restricted { get; set; }

        public PartyRestrictedMessage(uint partyId, bool restricted)
        {
            this.PartyId = partyId;
            this.Restricted = restricted;
        }

        public PartyRestrictedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(Restricted);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Restricted = reader.ReadBoolean();
        }

    }
}
