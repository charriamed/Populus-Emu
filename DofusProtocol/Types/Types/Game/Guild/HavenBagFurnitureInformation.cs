namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HavenBagFurnitureInformation
    {
        public const short Id  = 498;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort CellId { get; set; }
        public int FunitureId { get; set; }
        public sbyte Orientation { get; set; }

        public HavenBagFurnitureInformation(ushort cellId, int funitureId, sbyte orientation)
        {
            this.CellId = cellId;
            this.FunitureId = funitureId;
            this.Orientation = orientation;
        }

        public HavenBagFurnitureInformation() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(CellId);
            writer.WriteInt(FunitureId);
            writer.WriteSByte(Orientation);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            CellId = reader.ReadVarUShort();
            FunitureId = reader.ReadInt();
            Orientation = reader.ReadSByte();
        }

    }
}
