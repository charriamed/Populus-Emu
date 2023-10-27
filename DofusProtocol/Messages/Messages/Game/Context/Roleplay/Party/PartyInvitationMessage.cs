namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyInvitationMessage : AbstractPartyMessage
    {
        public new const uint Id = 5586;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte PartyType { get; set; }
        public string PartyName { get; set; }
        public sbyte MaxParticipants { get; set; }
        public ulong FromId { get; set; }
        public string FromName { get; set; }
        public ulong ToId { get; set; }

        public PartyInvitationMessage(uint partyId, sbyte partyType, string partyName, sbyte maxParticipants, ulong fromId, string fromName, ulong toId)
        {
            this.PartyId = partyId;
            this.PartyType = partyType;
            this.PartyName = partyName;
            this.MaxParticipants = maxParticipants;
            this.FromId = fromId;
            this.FromName = fromName;
            this.ToId = toId;
        }

        public PartyInvitationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(PartyType);
            writer.WriteUTF(PartyName);
            writer.WriteSByte(MaxParticipants);
            writer.WriteVarULong(FromId);
            writer.WriteUTF(FromName);
            writer.WriteVarULong(ToId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PartyType = reader.ReadSByte();
            PartyName = reader.ReadUTF();
            MaxParticipants = reader.ReadSByte();
            FromId = reader.ReadVarULong();
            FromName = reader.ReadUTF();
            ToId = reader.ReadVarULong();
        }

    }
}
