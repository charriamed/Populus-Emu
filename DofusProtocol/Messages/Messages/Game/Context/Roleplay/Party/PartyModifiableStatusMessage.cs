namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyModifiableStatusMessage : AbstractPartyMessage
    {
        public new const uint Id = 6277;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Enabled { get; set; }

        public PartyModifiableStatusMessage(uint partyId, bool enabled)
        {
            this.PartyId = partyId;
            this.Enabled = enabled;
        }

        public PartyModifiableStatusMessage() { }

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
