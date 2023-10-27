namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class DungeonPartyFinderAvailableDungeonsMessage : Message
    {
        public const uint Id = 6242;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort[] DungeonIds { get; set; }

        public DungeonPartyFinderAvailableDungeonsMessage(ushort[] dungeonIds)
        {
            this.DungeonIds = dungeonIds;
        }

        public DungeonPartyFinderAvailableDungeonsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)DungeonIds.Count());
            for (var dungeonIdsIndex = 0; dungeonIdsIndex < DungeonIds.Count(); dungeonIdsIndex++)
            {
                writer.WriteVarUShort(DungeonIds[dungeonIdsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var dungeonIdsCount = reader.ReadUShort();
            DungeonIds = new ushort[dungeonIdsCount];
            for (var dungeonIdsIndex = 0; dungeonIdsIndex < dungeonIdsCount; dungeonIdsIndex++)
            {
                DungeonIds[dungeonIdsIndex] = reader.ReadVarUShort();
            }
        }

    }
}
