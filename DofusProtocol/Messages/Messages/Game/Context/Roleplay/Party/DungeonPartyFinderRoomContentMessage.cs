namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class DungeonPartyFinderRoomContentMessage : Message
    {
        public const uint Id = 6247;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort DungeonId { get; set; }
        public DungeonPartyFinderPlayer[] Players { get; set; }

        public DungeonPartyFinderRoomContentMessage(ushort dungeonId, DungeonPartyFinderPlayer[] players)
        {
            this.DungeonId = dungeonId;
            this.Players = players;
        }

        public DungeonPartyFinderRoomContentMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(DungeonId);
            writer.WriteShort((short)Players.Count());
            for (var playersIndex = 0; playersIndex < Players.Count(); playersIndex++)
            {
                var objectToSend = Players[playersIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            DungeonId = reader.ReadVarUShort();
            var playersCount = reader.ReadUShort();
            Players = new DungeonPartyFinderPlayer[playersCount];
            for (var playersIndex = 0; playersIndex < playersCount; playersIndex++)
            {
                var objectToAdd = new DungeonPartyFinderPlayer();
                objectToAdd.Deserialize(reader);
                Players[playersIndex] = objectToAdd;
            }
        }

    }
}
