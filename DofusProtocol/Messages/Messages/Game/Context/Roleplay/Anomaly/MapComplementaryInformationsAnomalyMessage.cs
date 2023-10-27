namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class MapComplementaryInformationsAnomalyMessage : MapComplementaryInformationsDataMessage
    {
        public new const uint Id = 6828;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort Level { get; set; }
        public ulong ClosingTime { get; set; }

        public MapComplementaryInformationsAnomalyMessage(ushort subAreaId, double mapId, HouseInformations[] houses, GameRolePlayActorInformations[] actors, InteractiveElement[] interactiveElements, StatedElement[] statedElements, MapObstacle[] obstacles, FightCommonInformations[] fights, bool hasAggressiveMonsters, FightStartingPositions fightStartPositions, ushort level, ulong closingTime)
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
            this.Level = level;
            this.ClosingTime = closingTime;
        }

        public MapComplementaryInformationsAnomalyMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(Level);
            writer.WriteVarULong(ClosingTime);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Level = reader.ReadVarUShort();
            ClosingTime = reader.ReadVarULong();
        }

    }
}
