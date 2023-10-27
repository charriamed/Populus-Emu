namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MapCoordinatesExtended : MapCoordinatesAndId
    {
        public new const short Id = 176;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort SubAreaId { get; set; }

        public MapCoordinatesExtended(short worldX, short worldY, double mapId, ushort subAreaId)
        {
            this.WorldX = worldX;
            this.WorldY = worldY;
            this.MapId = mapId;
            this.SubAreaId = subAreaId;
        }

        public MapCoordinatesExtended() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(SubAreaId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            SubAreaId = reader.ReadVarUShort();
        }

    }
}
