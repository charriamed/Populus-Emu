namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class DungeonPartyFinderListenErrorMessage : Message
    {
        public const uint Id = 6248;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort DungeonId { get; set; }

        public DungeonPartyFinderListenErrorMessage(ushort dungeonId)
        {
            this.DungeonId = dungeonId;
        }

        public DungeonPartyFinderListenErrorMessage() { }

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
