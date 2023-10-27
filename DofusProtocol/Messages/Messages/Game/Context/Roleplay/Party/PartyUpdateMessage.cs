namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyUpdateMessage : AbstractPartyEventMessage
    {
        public new const uint Id = 5575;
        public override uint MessageId
        {
            get { return Id; }
        }
        public PartyMemberInformations MemberInformations { get; set; }

        public PartyUpdateMessage(uint partyId, PartyMemberInformations memberInformations)
        {
            this.PartyId = partyId;
            this.MemberInformations = memberInformations;
        }

        public PartyUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(MemberInformations.TypeId);
            MemberInformations.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            MemberInformations = ProtocolTypeManager.GetInstance<PartyMemberInformations>(reader.ReadShort());
            MemberInformations.Deserialize(reader);
        }

    }
}
