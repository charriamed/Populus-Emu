namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class MapObstacleUpdateMessage : Message
    {
        public const uint Id = 6051;
        public override uint MessageId
        {
            get { return Id; }
        }
        public MapObstacle[] Obstacles { get; set; }

        public MapObstacleUpdateMessage(MapObstacle[] obstacles)
        {
            this.Obstacles = obstacles;
        }

        public MapObstacleUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Obstacles.Count());
            for (var obstaclesIndex = 0; obstaclesIndex < Obstacles.Count(); obstaclesIndex++)
            {
                var objectToSend = Obstacles[obstaclesIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var obstaclesCount = reader.ReadUShort();
            Obstacles = new MapObstacle[obstaclesCount];
            for (var obstaclesIndex = 0; obstaclesIndex < obstaclesCount; obstaclesIndex++)
            {
                var objectToAdd = new MapObstacle();
                objectToAdd.Deserialize(reader);
                Obstacles[obstaclesIndex] = objectToAdd;
            }
        }

    }
}
