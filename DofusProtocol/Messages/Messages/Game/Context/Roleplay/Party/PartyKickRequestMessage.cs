namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyKickRequestMessage : AbstractPartyMessage
    {
        public new const uint Id = 5592;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong PlayerId { get; set; }

        public PartyKickRequestMessage(uint partyId, ulong playerId)
        {
            this.PartyId = partyId;
            this.PlayerId = playerId;
        }

        public PartyKickRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(PlayerId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PlayerId = reader.ReadVarULong();
        }

    }
}
