namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PrismGeolocalizedInformation : PrismSubareaEmptyInfo
    {
        public new const short Id = 434;
        public override short TypeId
        {
            get { return Id; }
        }
        public short WorldX { get; set; }
        public short WorldY { get; set; }
        public double MapId { get; set; }
        public PrismInformation Prism { get; set; }

        public PrismGeolocalizedInformation(ushort subAreaId, uint allianceId, short worldX, short worldY, double mapId, PrismInformation prism)
        {
            this.SubAreaId = subAreaId;
            this.AllianceId = allianceId;
            this.WorldX = worldX;
            this.WorldY = worldY;
            this.MapId = mapId;
            this.Prism = prism;
        }

        public PrismGeolocalizedInformation() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(WorldX);
            writer.WriteShort(WorldY);
            writer.WriteDouble(MapId);
            writer.WriteShort(Prism.TypeId);
            Prism.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            WorldX = reader.ReadShort();
            WorldY = reader.ReadShort();
            MapId = reader.ReadDouble();
            Prism = ProtocolTypeManager.GetInstance<PrismInformation>(reader.ReadShort());
            Prism.Deserialize(reader);
        }

    }
}
