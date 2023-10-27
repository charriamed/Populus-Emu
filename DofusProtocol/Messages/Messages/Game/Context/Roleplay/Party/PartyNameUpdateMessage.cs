namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyNameUpdateMessage : AbstractPartyMessage
    {
        public new const uint Id = 6502;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string PartyName { get; set; }

        public PartyNameUpdateMessage(uint partyId, string partyName)
        {
            this.PartyId = partyId;
            this.PartyName = partyName;
        }

        public PartyNameUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(PartyName);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PartyName = reader.ReadUTF();
        }

    }
}
