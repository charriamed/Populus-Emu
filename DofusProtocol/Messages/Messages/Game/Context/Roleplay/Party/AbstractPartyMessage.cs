namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AbstractPartyMessage : Message
    {
        public const uint Id = 6274;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint PartyId { get; set; }

        public AbstractPartyMessage(uint partyId)
        {
            this.PartyId = partyId;
        }

        public AbstractPartyMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(PartyId);
        }

        public override void Deserialize(IDataReader reader)
        {
            PartyId = reader.ReadVarUInt();
        }

    }
}
