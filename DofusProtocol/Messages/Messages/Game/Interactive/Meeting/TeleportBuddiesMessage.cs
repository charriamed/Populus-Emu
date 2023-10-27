namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TeleportBuddiesMessage : Message
    {
        public const uint Id = 6289;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort DungeonId { get; set; }

        public TeleportBuddiesMessage(ushort dungeonId)
        {
            this.DungeonId = dungeonId;
        }

        public TeleportBuddiesMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(DungeonId);
        }

        public override void Deserialize(IDataReader reader)
        {
            DungeonId = reader.ReadVarUShort();
        }

    }
}
