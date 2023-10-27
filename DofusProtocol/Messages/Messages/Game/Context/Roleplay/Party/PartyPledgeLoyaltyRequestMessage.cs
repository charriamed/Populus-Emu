namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyPledgeLoyaltyRequestMessage : AbstractPartyMessage
    {
        public new const uint Id = 6269;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Loyal { get; set; }

        public PartyPledgeLoyaltyRequestMessage(uint partyId, bool loyal)
        {
            this.PartyId = partyId;
            this.Loyal = loyal;
        }

        public PartyPledgeLoyaltyRequestMessage() { }

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
