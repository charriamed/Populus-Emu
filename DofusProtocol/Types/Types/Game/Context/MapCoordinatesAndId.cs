namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MapCoordinatesAndId : MapCoordinates
    {
        public new const short Id = 392;
        public override short TypeId
        {
            get { return Id; }
        }
        public double MapId { get; set; }

        public MapCoordinatesAndId(short worldX, short worldY, double mapId)
        {
            this.WorldX = worldX;
            this.WorldY = worldY;
            this.MapId = mapId;
        }

        public MapCoordinatesAndId() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(MapId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            MapId = reader.ReadDouble();
        }

    }
}
