namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyMemberInStandardFightMessage : AbstractPartyMemberInFightMessage
    {
        public new const uint Id = 6826;
        public override uint MessageId
        {
            get { return Id; }
        }
        public MapCoordinatesExtended FightMap { get; set; }

        public PartyMemberInStandardFightMessage(uint partyId, sbyte reason, ulong memberId, int memberAccountId, string memberName, ushort fightId, short timeBeforeFightStart, MapCoordinatesExtended fightMap)
        {
            this.PartyId = partyId;
            this.Reason = reason;
            this.MemberId = memberId;
            this.MemberAccountId = memberAccountId;
            this.MemberName = memberName;
            this.FightId = fightId;
            this.TimeBeforeFightStart = timeBeforeFightStart;
            this.FightMap = fightMap;
        }

        public PartyMemberInStandardFightMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            FightMap.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            FightMap = new MapCoordinatesExtended();
            FightMap.Deserialize(reader);
        }

    }
}
