namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TeleportToBuddyCloseMessage : Message
    {
        public const uint Id = 6303;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort DungeonId { get; set; }
        public ulong BuddyId { get; set; }

        public TeleportToBuddyCloseMessage(ushort dungeonId, ulong buddyId)
        {
            this.DungeonId = dungeonId;
            this.BuddyId = buddyId;
        }

        public TeleportToBuddyCloseMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(DungeonId);
            writer.WriteVarULong(BuddyId);
        }

        public override void Deserialize(IDataReader reader)
        {
            DungeonId = reader.ReadVarUShort();
            BuddyId = reader.ReadVarULong();
        }

    }
}
