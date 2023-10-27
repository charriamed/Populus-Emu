namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TaxCollectorBasicInformations
    {
        public const short Id  = 96;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort FirstNameId { get; set; }
        public ushort LastNameId { get; set; }
        public short WorldX { get; set; }
        public short WorldY { get; set; }
        public double MapId { get; set; }
        public ushort SubAreaId { get; set; }

        public TaxCollectorBasicInformations(ushort firstNameId, ushort lastNameId, short worldX, short worldY, double mapId, ushort subAreaId)
        {
            this.FirstNameId = firstNameId;
            this.LastNameId = lastNameId;
            this.WorldX = worldX;
            this.WorldY = worldY;
            this.MapId = mapId;
            this.SubAreaId = subAreaId;
        }

        public TaxCollectorBasicInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(FirstNameId);
            writer.WriteVarUShort(LastNameId);
            writer.WriteShort(WorldX);
            writer.WriteShort(WorldY);
            writer.WriteDouble(MapId);
            writer.WriteVarUShort(SubAreaId);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            FirstNameId = reader.ReadVarUShort();
            LastNameId = reader.ReadVarUShort();
            WorldX = reader.ReadShort();
            WorldY = reader.ReadShort();
            MapId = reader.ReadDouble();
            SubAreaId = reader.ReadVarUShort();
        }

    }
}
