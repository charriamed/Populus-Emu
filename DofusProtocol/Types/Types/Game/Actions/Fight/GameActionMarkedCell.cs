namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionMarkedCell
    {
        public const short Id  = 85;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort CellId { get; set; }
        public sbyte ZoneSize { get; set; }
        public int CellColor { get; set; }
        public sbyte CellsType { get; set; }

        public GameActionMarkedCell(ushort cellId, sbyte zoneSize, int cellColor, sbyte cellsType)
        {
            this.CellId = cellId;
            this.ZoneSize = zoneSize;
            this.CellColor = cellColor;
            this.CellsType = cellsType;
        }

        public GameActionMarkedCell() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(CellId);
            writer.WriteSByte(ZoneSize);
            writer.WriteInt(CellColor);
            writer.WriteSByte(CellsType);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            CellId = reader.ReadVarUShort();
            ZoneSize = reader.ReadSByte();
            CellColor = reader.ReadInt();
            CellsType = reader.ReadSByte();
        }

    }
}
