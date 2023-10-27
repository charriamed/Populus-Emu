namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class DungeonPartyFinderRoomContentUpdateMessage : Message
    {
        public const uint Id = 6250;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort DungeonId { get; set; }
        public DungeonPartyFinderPlayer[] AddedPlayers { get; set; }
        public ulong[] RemovedPlayersIds { get; set; }

        public DungeonPartyFinderRoomContentUpdateMessage(ushort dungeonId, DungeonPartyFinderPlayer[] addedPlayers, ulong[] removedPlayersIds)
        {
            this.DungeonId = dungeonId;
            this.AddedPlayers = addedPlayers;
            this.RemovedPlayersIds = removedPlayersIds;
        }

        public DungeonPartyFinderRoomContentUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(DungeonId);
            writer.WriteShort((short)AddedPlayers.Count());
            for (var addedPlayersIndex = 0; addedPlayersIndex < AddedPlayers.Count(); addedPlayersIndex++)
            {
                var objectToSend = AddedPlayers[addedPlayersIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)RemovedPlayersIds.Count());
            for (var removedPlayersIdsIndex = 0; removedPlayersIdsIndex < RemovedPlayersIds.Count(); removedPlayersIdsIndex++)
            {
                writer.WriteVarULong(RemovedPlayersIds[removedPlayersIdsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            DungeonId = reader.ReadVarUShort();
            var addedPlayersCount = reader.ReadUShort();
            AddedPlayers = new DungeonPartyFinderPlayer[addedPlayersCount];
            for (var addedPlayersIndex = 0; addedPlayersIndex < addedPlayersCount; addedPlayersIndex++)
            {
                var objectToAdd = new DungeonPartyFinderPlayer();
                objectToAdd.Deserialize(reader);
                AddedPlayers[addedPlayersIndex] = objectToAdd;
            }
            var removedPlayersIdsCount = reader.ReadUShort();
            RemovedPlayersIds = new ulong[removedPlayersIdsCount];
            for (var removedPlayersIdsIndex = 0; removedPlayersIdsIndex < removedPlayersIdsCount; removedPlayersIdsIndex++)
            {
                RemovedPlayersIds[removedPlayersIdsIndex] = reader.ReadVarULong();
            }
        }

    }
}
