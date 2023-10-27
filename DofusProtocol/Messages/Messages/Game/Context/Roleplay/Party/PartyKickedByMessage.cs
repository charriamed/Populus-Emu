namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyKickedByMessage : AbstractPartyMessage
    {
        public new const uint Id = 5590;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong KickerId { get; set; }

        public PartyKickedByMessage(uint partyId, ulong kickerId)
        {
            this.PartyId = partyId;
            this.KickerId = kickerId;
        }

        public PartyKickedByMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(KickerId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            KickerId = reader.ReadVarULong();
        }

    }
}
