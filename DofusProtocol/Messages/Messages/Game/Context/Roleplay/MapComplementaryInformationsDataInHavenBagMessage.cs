namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class MapComplementaryInformationsDataInHavenBagMessage : MapComplementaryInformationsDataMessage
    {
        public new const uint Id = 6622;
        public override uint MessageId
        {
            get { return Id; }
        }
        public CharacterMinimalInformations OwnerInformations { get; set; }
        public sbyte Theme { get; set; }
        public sbyte RoomId { get; set; }
        public sbyte MaxRoomId { get; set; }

        public MapComplementaryInformationsDataInHavenBagMessage(ushort subAreaId, double mapId, HouseInformations[] houses, GameRolePlayActorInformations[] actors, InteractiveElement[] interactiveElements, StatedElement[] statedElements, MapObstacle[] obstacles, FightCommonInformations[] fights, bool hasAggressiveMonsters, FightStartingPositions fightStartPositions, CharacterMinimalInformations ownerInformations, sbyte theme, sbyte roomId, sbyte maxRoomId)
        {
            this.SubAreaId = subAreaId;
            this.MapId = mapId;
            this.Houses = houses;
            this.Actors = actors;
            this.InteractiveElements = interactiveElements;
            this.StatedElements = statedElements;
            this.Obstacles = obstacles;
            this.Fights = fights;
            this.HasAggressiveMonsters = hasAggressiveMonsters;
            this.FightStartPositions = fightStartPositions;
            this.OwnerInformations = ownerInformations;
            this.Theme = theme;
            this.RoomId = roomId;
            this.MaxRoomId = maxRoomId;
        }

        public MapComplementaryInformationsDataInHavenBagMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            OwnerInformations.Serialize(writer);
            writer.WriteSByte(Theme);
            writer.WriteSByte(RoomId);
            writer.WriteSByte(MaxRoomId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            OwnerInformations = new CharacterMinimalInformations();
            OwnerInformations.Deserialize(reader);
            Theme = reader.ReadSByte();
            RoomId = reader.ReadSByte();
            MaxRoomId = reader.ReadSByte();
        }

    }
}
