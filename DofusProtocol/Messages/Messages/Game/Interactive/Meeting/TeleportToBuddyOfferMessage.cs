namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TeleportToBuddyOfferMessage : Message
    {
        public const uint Id = 6287;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort DungeonId { get; set; }
        public ulong BuddyId { get; set; }
        public uint TimeLeft { get; set; }

        public TeleportToBuddyOfferMessage(ushort dungeonId, ulong buddyId, uint timeLeft)
        {
            this.DungeonId = dungeonId;
            this.BuddyId = buddyId;
            this.TimeLeft = timeLeft;
        }

        public TeleportToBuddyOfferMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(DungeonId);
            writer.WriteVarULong(BuddyId);
            writer.WriteVarUInt(TimeLeft);
        }

        public override void Deserialize(IDataReader reader)
        {
            DungeonId = reader.ReadVarUShort();
            BuddyId = reader.ReadVarULong();
            TimeLeft = reader.ReadVarUInt();
        }

    }
}
