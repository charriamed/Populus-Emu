namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyCannotJoinErrorMessage : AbstractPartyMessage
    {
        public new const uint Id = 5583;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Reason { get; set; }

        public PartyCannotJoinErrorMessage(uint partyId, sbyte reason)
        {
            this.PartyId = partyId;
            this.Reason = reason;
        }

        public PartyCannotJoinErrorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(Reason);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Reason = reader.ReadSByte();
        }

    }
}
