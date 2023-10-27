namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TeleportToBuddyAnswerMessage : Message
    {
        public const uint Id = 6293;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort DungeonId { get; set; }
        public ulong BuddyId { get; set; }
        public bool Accept { get; set; }

        public TeleportToBuddyAnswerMessage(ushort dungeonId, ulong buddyId, bool accept)
        {
            this.DungeonId = dungeonId;
            this.BuddyId = buddyId;
            this.Accept = accept;
        }

        public TeleportToBuddyAnswerMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(DungeonId);
            writer.WriteVarULong(BuddyId);
            writer.WriteBoolean(Accept);
        }

        public override void Deserialize(IDataReader reader)
        {
            DungeonId = reader.ReadVarUShort();
            BuddyId = reader.ReadVarULong();
            Accept = reader.ReadBoolean();
        }

    }
}
