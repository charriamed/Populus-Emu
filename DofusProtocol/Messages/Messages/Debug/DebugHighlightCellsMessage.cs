namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class DebugHighlightCellsMessage : Message
    {
        public const uint Id = 2001;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double color;
        public IEnumerable<ushort> cells;

        public DebugHighlightCellsMessage(double color, IEnumerable<ushort> cells)
        {
            this.color = color;
            this.cells = cells;
        }

        public DebugHighlightCellsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(color);
            writer.WriteShort((short)cells.Count());
            foreach (var objectToSend in cells)
            {
                writer.WriteVarUShort(objectToSend);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            color = reader.ReadDouble();
            var cellsCount = reader.ReadUShort();
            var cells_ = new ushort[cellsCount];
            for (var cellsIndex = 0; cellsIndex < cellsCount; cellsIndex++)
            {
                cells_[cellsIndex] = reader.ReadVarUShort();
            }
           cells = cells_;
        }

    }
}
