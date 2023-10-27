namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyMemberInBreachFightMessage : AbstractPartyMemberInFightMessage
    {
        public new const uint Id = 6824;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint Floor { get; set; }
        public sbyte Room { get; set; }

        public PartyMemberInBreachFightMessage(uint partyId, sbyte reason, ulong memberId, int memberAccountId, string memberName, ushort fightId, short timeBeforeFightStart, uint floor, sbyte room)
        {
            this.PartyId = partyId;
            this.Reason = reason;
            this.MemberId = memberId;
            this.MemberAccountId = memberAccountId;
            this.MemberName = memberName;
            this.FightId = fightId;
            this.TimeBeforeFightStart = timeBeforeFightStart;
            this.Floor = floor;
            this.Room = room;
        }

        public PartyMemberInBreachFightMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(Floor);
            writer.WriteSByte(Room);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Floor = reader.ReadVarUInt();
            Room = reader.ReadSByte();
        }

    }
}
