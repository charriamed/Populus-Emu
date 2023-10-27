namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyStopFollowRequestMessage : AbstractPartyMessage
    {
        public new const uint Id = 5574;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong PlayerId { get; set; }

        public PartyStopFollowRequestMessage(uint partyId, ulong playerId)
        {
            this.PartyId = partyId;
            this.PlayerId = playerId;
        }

        public PartyStopFollowRequestMessage() { }

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
