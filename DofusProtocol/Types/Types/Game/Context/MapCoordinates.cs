namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MapCoordinates
    {
        public const short Id  = 174;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public short WorldX { get; set; }
        public short WorldY { get; set; }

        public MapCoordinates(short worldX, short worldY)
        {
            this.WorldX = worldX;
            this.WorldY = worldY;
        }

        public MapCoordinates() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort(WorldX);
            writer.WriteShort(WorldY);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            WorldX = reader.ReadShort();
            WorldY = reader.ReadShort();
        }

    }
}
