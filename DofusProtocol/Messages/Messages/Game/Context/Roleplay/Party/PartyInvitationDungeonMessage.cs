namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyInvitationDungeonMessage : PartyInvitationMessage
    {
        public new const uint Id = 6244;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort DungeonId { get; set; }

        public PartyInvitationDungeonMessage(uint partyId, sbyte partyType, string partyName, sbyte maxParticipants, ulong fromId, string fromName, ulong toId, ushort dungeonId)
        {
            this.PartyId = partyId;
            this.PartyType = partyType;
            this.PartyName = partyName;
            this.MaxParticipants = maxParticipants;
            this.FromId = fromId;
            this.FromName = fromName;
            this.ToId = toId;
            this.DungeonId = dungeonId;
        }

        public PartyInvitationDungeonMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(DungeonId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            DungeonId = reader.ReadVarUShort();
        }

    }
}
