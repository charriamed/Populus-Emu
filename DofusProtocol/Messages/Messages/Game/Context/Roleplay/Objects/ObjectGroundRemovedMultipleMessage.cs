namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectGroundRemovedMultipleMessage : Message
    {
        public const uint Id = 5944;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort[] Cells { get; set; }

        public ObjectGroundRemovedMultipleMessage(ushort[] cells)
        {
            this.Cells = cells;
        }

        public ObjectGroundRemovedMultipleMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Cells.Count());
            for (var cellsIndex = 0; cellsIndex < Cells.Count(); cellsIndex++)
            {
                writer.WriteVarUShort(Cells[cellsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var cellsCount = reader.ReadUShort();
            Cells = new ushort[cellsCount];
            for (var cellsIndex = 0; cellsIndex < cellsCount; cellsIndex++)
            {
                Cells[cellsIndex] = reader.ReadVarUShort();
            }
        }

    }
}
