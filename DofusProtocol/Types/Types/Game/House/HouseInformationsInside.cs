namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HouseInformationsInside : HouseInformations
    {
        public new const short Id = 218;
        public override short TypeId
        {
            get { return Id; }
        }
        public HouseInstanceInformations HouseInfos { get; set; }
        public short WorldX { get; set; }
        public short WorldY { get; set; }

        public HouseInformationsInside(uint houseId, ushort modelId, HouseInstanceInformations houseInfos, short worldX, short worldY)
        {
            this.HouseId = houseId;
            this.ModelId = modelId;
            this.HouseInfos = houseInfos;
            this.WorldX = worldX;
            this.WorldY = worldY;
        }

        public HouseInformationsInside() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(HouseInfos.TypeId);
            HouseInfos.Serialize(writer);
            writer.WriteShort(WorldX);
            writer.WriteShort(WorldY);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            HouseInfos = ProtocolTypeManager.GetInstance<HouseInstanceInformations>(reader.ReadShort());
            HouseInfos.Deserialize(reader);
            WorldX = reader.ReadShort();
            WorldY = reader.ReadShort();
        }

    }
}
