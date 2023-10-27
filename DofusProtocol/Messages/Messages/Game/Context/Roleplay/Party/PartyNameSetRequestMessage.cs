namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyNameSetRequestMessage : AbstractPartyMessage
    {
        public new const uint Id = 6503;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string PartyName { get; set; }

        public PartyNameSetRequestMessage(uint partyId, string partyName)
        {
            this.PartyId = partyId;
            this.PartyName = partyName;
        }

        public PartyNameSetRequestMessage() { }

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
