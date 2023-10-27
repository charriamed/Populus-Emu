namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AbstractPartyMemberInFightMessage : AbstractPartyMessage
    {
        public new const uint Id = 6825;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Reason { get; set; }
        public ulong MemberId { get; set; }
        public int MemberAccountId { get; set; }
        public string MemberName { get; set; }
        public ushort FightId { get; set; }
        public short TimeBeforeFightStart { get; set; }

        public AbstractPartyMemberInFightMessage(uint partyId, sbyte reason, ulong memberId, int memberAccountId, string memberName, ushort fightId, short timeBeforeFightStart)
        {
            this.PartyId = partyId;
            this.Reason = reason;
            this.MemberId = memberId;
            this.MemberAccountId = memberAccountId;
            this.MemberName = memberName;
            this.FightId = fightId;
            this.TimeBeforeFightStart = timeBeforeFightStart;
        }

        public AbstractPartyMemberInFightMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(Reason);
            writer.WriteVarULong(MemberId);
            writer.WriteInt(MemberAccountId);
            writer.WriteUTF(MemberName);
            writer.WriteVarUShort(FightId);
            writer.WriteVarShort(TimeBeforeFightStart);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Reason = reader.ReadSByte();
            MemberId = reader.ReadVarULong();
            MemberAccountId = reader.ReadInt();
            MemberName = reader.ReadUTF();
            FightId = reader.ReadVarUShort();
            TimeBeforeFightStart = reader.ReadVarShort();
        }

    }
}
