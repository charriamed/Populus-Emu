namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class EntityDispositionInformations
    {
        public const short Id  = 60;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public short CellId { get; set; }
        public sbyte Direction { get; set; }

        public EntityDispositionInformations(short cellId, sbyte direction)
        {
            this.CellId = cellId;
            this.Direction = direction;
        }

        public EntityDispositionInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort(CellId);
            writer.WriteSByte(Direction);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            CellId = reader.ReadShort();
            Direction = reader.ReadSByte();
        }

    }
}
