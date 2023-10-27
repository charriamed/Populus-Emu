namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyMemberEjectedMessage : PartyMemberRemoveMessage
    {
        public new const uint Id = 6252;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong KickerId { get; set; }

        public PartyMemberEjectedMessage(uint partyId, ulong leavingPlayerId, ulong kickerId)
        {
            this.PartyId = partyId;
            this.LeavingPlayerId = leavingPlayerId;
            this.KickerId = kickerId;
        }

        public PartyMemberEjectedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(KickerId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            KickerId = reader.ReadVarULong();
        }

    }
}
