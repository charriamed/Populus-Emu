namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class MapComplementaryInformationsDataInHouseMessage : MapComplementaryInformationsDataMessage
    {
        public new const uint Id = 6130;
        public override uint MessageId
        {
            get { return Id; }
        }
        public HouseInformationsInside CurrentHouse { get; set; }

        public MapComplementaryInformationsDataInHouseMessage(ushort subAreaId, double mapId, HouseInformations[] houses, GameRolePlayActorInformations[] actors, InteractiveElement[] interactiveElements, StatedElement[] statedElements, MapObstacle[] obstacles, FightCommonInformations[] fights, bool hasAggressiveMonsters, FightStartingPositions fightStartPositions, HouseInformationsInside currentHouse)
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
            this.CurrentHouse = currentHouse;
        }

        public MapComplementaryInformationsDataInHouseMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            CurrentHouse.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            CurrentHouse = new HouseInformationsInside();
            CurrentHouse.Deserialize(reader);
        }

    }
}
