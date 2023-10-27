namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MapFightStartPositionsUpdateMessage : Message
    {
        public const uint Id = 6716;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double MapId { get; set; }
        public FightStartingPositions FightStartPositions { get; set; }

        public MapFightStartPositionsUpdateMessage(double mapId, FightStartingPositions fightStartPositions)
        {
            this.MapId = mapId;
            this.FightStartPositions = fightStartPositions;
        }

        public MapFightStartPositionsUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(MapId);
            FightStartPositions.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            MapId = reader.ReadDouble();
            FightStartPositions = new FightStartingPositions();
            FightStartPositions.Deserialize(reader);
        }

    }
}
