namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class DungeonPartyFinderListenRequestMessage : Message
    {
        public const uint Id = 6246;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort DungeonId { get; set; }

        public DungeonPartyFinderListenRequestMessage(ushort dungeonId)
        {
            this.DungeonId = dungeonId;
        }

        public DungeonPartyFinderListenRequestMessage() { }

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
