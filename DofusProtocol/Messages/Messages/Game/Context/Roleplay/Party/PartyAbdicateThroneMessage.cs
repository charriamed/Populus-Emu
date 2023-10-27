namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyAbdicateThroneMessage : AbstractPartyMessage
    {
        public new const uint Id = 6080;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong PlayerId { get; set; }

        public PartyAbdicateThroneMessage(uint partyId, ulong playerId)
        {
            this.PartyId = partyId;
            this.PlayerId = playerId;
        }

        public PartyAbdicateThroneMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(PlayerId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PlayerId = reader.ReadVarULong();
        }

    }
}
