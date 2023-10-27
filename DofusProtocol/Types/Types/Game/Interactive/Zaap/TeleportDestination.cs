namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TeleportDestination
    {
        public const short Id  = 563;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte Type { get; set; }
        public double MapId { get; set; }
        public ushort SubAreaId { get; set; }
        public ushort Level { get; set; }
        public ushort Cost { get; set; }

        public TeleportDestination(sbyte type, double mapId, ushort subAreaId, ushort level, ushort cost)
        {
            this.Type = type;
            this.MapId = mapId;
            this.SubAreaId = subAreaId;
            this.Level = level;
            this.Cost = cost;
        }

        public TeleportDestination() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Type);
            writer.WriteDouble(MapId);
            writer.WriteVarUShort(SubAreaId);
            writer.WriteVarUShort(Level);
            writer.WriteVarUShort(Cost);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            Type = reader.ReadSByte();
            MapId = reader.ReadDouble();
            SubAreaId = reader.ReadVarUShort();
            Level = reader.ReadVarUShort();
            Cost = reader.ReadVarUShort();
        }

    }
}
