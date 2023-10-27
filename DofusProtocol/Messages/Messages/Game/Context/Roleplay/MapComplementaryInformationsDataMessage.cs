namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class MapComplementaryInformationsDataMessage : Message
    {
        public const uint Id = 226;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort SubAreaId { get; set; }
        public double MapId { get; set; }
        public HouseInformations[] Houses { get; set; }
        public GameRolePlayActorInformations[] Actors { get; set; }
        public InteractiveElement[] InteractiveElements { get; set; }
        public StatedElement[] StatedElements { get; set; }
        public MapObstacle[] Obstacles { get; set; }
        public FightCommonInformations[] Fights { get; set; }
        public bool HasAggressiveMonsters { get; set; }
        public FightStartingPositions FightStartPositions { get; set; }

        public MapComplementaryInformationsDataMessage(ushort subAreaId, double mapId, HouseInformations[] houses, GameRolePlayActorInformations[] actors, InteractiveElement[] interactiveElements, StatedElement[] statedElements, MapObstacle[] obstacles, FightCommonInformations[] fights, bool hasAggressiveMonsters, FightStartingPositions fightStartPositions)
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
        }

        public MapComplementaryInformationsDataMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SubAreaId);
            writer.WriteDouble(MapId);
            writer.WriteShort((short)Houses.Count());
            for (var housesIndex = 0; housesIndex < Houses.Count(); housesIndex++)
            {
                var objectToSend = Houses[housesIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)Actors.Count());
            for (var actorsIndex = 0; actorsIndex < Actors.Count(); actorsIndex++)
            {
                var objectToSend = Actors[actorsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)InteractiveElements.Count());
            for (var interactiveElementsIndex = 0; interactiveElementsIndex < InteractiveElements.Count(); interactiveElementsIndex++)
            {
                var objectToSend = InteractiveElements[interactiveElementsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)StatedElements.Count());
            for (var statedElementsIndex = 0; statedElementsIndex < StatedElements.Count(); statedElementsIndex++)
            {
                var objectToSend = StatedElements[statedElementsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)Obstacles.Count());
            for (var obstaclesIndex = 0; obstaclesIndex < Obstacles.Count(); obstaclesIndex++)
            {
                var objectToSend = Obstacles[obstaclesIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)Fights.Count());
            for (var fightsIndex = 0; fightsIndex < Fights.Count(); fightsIndex++)
            {
                var objectToSend = Fights[fightsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteBoolean(HasAggressiveMonsters);
            FightStartPositions.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            SubAreaId = reader.ReadVarUShort();
            MapId = reader.ReadDouble();
            var housesCount = reader.ReadUShort();
            Houses = new HouseInformations[housesCount];
            for (var housesIndex = 0; housesIndex < housesCount; housesIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<HouseInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Houses[housesIndex] = objectToAdd;
            }
            var actorsCount = reader.ReadUShort();
            Actors = new GameRolePlayActorInformations[actorsCount];
            for (var actorsIndex = 0; actorsIndex < actorsCount; actorsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<GameRolePlayActorInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Actors[actorsIndex] = objectToAdd;
            }
            var interactiveElementsCount = reader.ReadUShort();
            InteractiveElements = new InteractiveElement[interactiveElementsCount];
            for (var interactiveElementsIndex = 0; interactiveElementsIndex < interactiveElementsCount; interactiveElementsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<InteractiveElement>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                InteractiveElements[interactiveElementsIndex] = objectToAdd;
            }
            var statedElementsCount = reader.ReadUShort();
            StatedElements = new StatedElement[statedElementsCount];
            for (var statedElementsIndex = 0; statedElementsIndex < statedElementsCount; statedElementsIndex++)
            {
                var objectToAdd = new StatedElement();
                objectToAdd.Deserialize(reader);
                StatedElements[statedElementsIndex] = objectToAdd;
            }
            var obstaclesCount = reader.ReadUShort();
            Obstacles = new MapObstacle[obstaclesCount];
            for (var obstaclesIndex = 0; obstaclesIndex < obstaclesCount; obstaclesIndex++)
            {
                var objectToAdd = new MapObstacle();
                objectToAdd.Deserialize(reader);
                Obstacles[obstaclesIndex] = objectToAdd;
            }
            var fightsCount = reader.ReadUShort();
            Fights = new FightCommonInformations[fightsCount];
            for (var fightsIndex = 0; fightsIndex < fightsCount; fightsIndex++)
            {
                var objectToAdd = new FightCommonInformations();
                objectToAdd.Deserialize(reader);
                Fights[fightsIndex] = objectToAdd;
            }
            HasAggressiveMonsters = reader.ReadBoolean();
            FightStartPositions = new FightStartingPositions();
            FightStartPositions.Deserialize(reader);
        }

    }
}
