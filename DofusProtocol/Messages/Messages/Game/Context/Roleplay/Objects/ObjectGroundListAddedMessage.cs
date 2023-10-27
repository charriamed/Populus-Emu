namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectGroundListAddedMessage : Message
    {
        public const uint Id = 5925;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort[] Cells { get; set; }
        public ushort[] ReferenceIds { get; set; }

        public ObjectGroundListAddedMessage(ushort[] cells, ushort[] referenceIds)
        {
            this.Cells = cells;
            this.ReferenceIds = referenceIds;
        }

        public ObjectGroundListAddedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Cells.Count());
            for (var cellsIndex = 0; cellsIndex < Cells.Count(); cellsIndex++)
            {
                writer.WriteVarUShort(Cells[cellsIndex]);
            }
            writer.WriteShort((short)ReferenceIds.Count());
            for (var referenceIdsIndex = 0; referenceIdsIndex < ReferenceIds.Count(); referenceIdsIndex++)
            {
                writer.WriteVarUShort(ReferenceIds[referenceIdsIndex]);
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
            var referenceIdsCount = reader.ReadUShort();
            ReferenceIds = new ushort[referenceIdsCount];
            for (var referenceIdsIndex = 0; referenceIdsIndex < referenceIdsCount; referenceIdsIndex++)
            {
                ReferenceIds[referenceIdsIndex] = reader.ReadVarUShort();
            }
        }

    }
}
