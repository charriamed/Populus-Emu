namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyLocateMembersRequestMessage : AbstractPartyMessage
    {
        public new const uint Id = 5587;
        public override uint MessageId
        {
            get { return Id; }
        }

        public PartyLocateMembersRequestMessage(uint partyId)
        {
            this.PartyId = partyId;
        }

        public PartyLocateMembersRequestMessage() { }

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
