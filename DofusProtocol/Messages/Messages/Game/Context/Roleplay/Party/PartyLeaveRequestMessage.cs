namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyLeaveRequestMessage : AbstractPartyMessage
    {
        public new const uint Id = 5593;
        public override uint MessageId
        {
            get { return Id; }
        }

        public PartyLeaveRequestMessage(uint partyId)
        {
            this.PartyId = partyId;
        }

        public PartyLeaveRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }

    }
}
