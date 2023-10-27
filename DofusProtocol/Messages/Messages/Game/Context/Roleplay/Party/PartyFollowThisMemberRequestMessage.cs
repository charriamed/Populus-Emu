namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyFollowThisMemberRequestMessage : PartyFollowMemberRequestMessage
    {
        public new const uint Id = 5588;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Enabled { get; set; }

        public PartyFollowThisMemberRequestMessage(uint partyId, ulong playerId, bool enabled)
        {
            this.PartyId = partyId;
            this.PlayerId = playerId;
            this.Enabled = enabled;
        }

        public PartyFollowThisMemberRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(Enabled);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Enabled = reader.ReadBoolean();
        }

    }
}
