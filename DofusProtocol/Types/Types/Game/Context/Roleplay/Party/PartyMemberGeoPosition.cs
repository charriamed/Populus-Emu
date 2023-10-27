namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyMemberGeoPosition
    {
        public const short Id  = 378;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public int MemberId { get; set; }
        public short WorldX { get; set; }
        public short WorldY { get; set; }
        public double MapId { get; set; }
        public ushort SubAreaId { get; set; }

        public PartyMemberGeoPosition(int memberId, short worldX, short worldY, double mapId, ushort subAreaId)
        {
            this.MemberId = memberId;
            this.WorldX = worldX;
            this.WorldY = worldY;
            this.MapId = mapId;
            this.SubAreaId = subAreaId;
        }

        public PartyMemberGeoPosition() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt(MemberId);
            writer.WriteShort(WorldX);
            writer.WriteShort(WorldY);
            writer.WriteDouble(MapId);
            writer.WriteVarUShort(SubAreaId);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            MemberId = reader.ReadInt();
            WorldX = reader.ReadShort();
            WorldY = reader.ReadShort();
            MapId = reader.ReadDouble();
            SubAreaId = reader.ReadVarUShort();
        }

    }
}
