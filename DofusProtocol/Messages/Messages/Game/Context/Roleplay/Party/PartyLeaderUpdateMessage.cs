namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyLeaderUpdateMessage : AbstractPartyEventMessage
    {
        public new const uint Id = 5578;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong PartyLeaderId { get; set; }

        public PartyLeaderUpdateMessage(uint partyId, ulong partyLeaderId)
        {
            this.PartyId = partyId;
            this.PartyLeaderId = partyLeaderId;
        }

        public PartyLeaderUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(PartyLeaderId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PartyLeaderId = reader.ReadVarULong();
        }

    }
}
