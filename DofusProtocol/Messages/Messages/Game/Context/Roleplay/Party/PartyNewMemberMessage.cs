namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyNewMemberMessage : PartyUpdateMessage
    {
        public new const uint Id = 6306;
        public override uint MessageId
        {
            get { return Id; }
        }

        public PartyNewMemberMessage(uint partyId, PartyMemberInformations memberInformations)
        {
            this.PartyId = partyId;
            this.MemberInformations = memberInformations;
        }

        public PartyNewMemberMessage() { }

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
