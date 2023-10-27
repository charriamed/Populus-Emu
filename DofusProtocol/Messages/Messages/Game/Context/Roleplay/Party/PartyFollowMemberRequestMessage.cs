namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyFollowMemberRequestMessage : AbstractPartyMessage
    {
        public new const uint Id = 5577;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong PlayerId { get; set; }

        public PartyFollowMemberRequestMessage(uint partyId, ulong playerId)
        {
            this.PartyId = partyId;
            this.PlayerId = playerId;
        }

        public PartyFollowMemberRequestMessage() { }

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
