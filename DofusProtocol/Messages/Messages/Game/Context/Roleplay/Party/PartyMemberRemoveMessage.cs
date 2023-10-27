namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyMemberRemoveMessage : AbstractPartyEventMessage
    {
        public new const uint Id = 5579;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong LeavingPlayerId { get; set; }

        public PartyMemberRemoveMessage(uint partyId, ulong leavingPlayerId)
        {
            this.PartyId = partyId;
            this.LeavingPlayerId = leavingPlayerId;
        }

        public PartyMemberRemoveMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(LeavingPlayerId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            LeavingPlayerId = reader.ReadVarULong();
        }

    }
}
