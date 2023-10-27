namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyInvitationArenaRequestMessage : PartyInvitationRequestMessage
    {
        public new const uint Id = 6283;
        public override uint MessageId
        {
            get { return Id; }
        }

        public PartyInvitationArenaRequestMessage(string name)
        {
            this.Name = name;
        }

        public PartyInvitationArenaRequestMessage() { }

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
