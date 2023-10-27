namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyLoyaltyStatusMessage : AbstractPartyMessage
    {
        public new const uint Id = 6270;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Loyal { get; set; }

        public PartyLoyaltyStatusMessage(uint partyId, bool loyal)
        {
            this.PartyId = partyId;
            this.Loyal = loyal;
        }

        public PartyLoyaltyStatusMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(Loyal);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Loyal = reader.ReadBoolean();
        }

    }
}
