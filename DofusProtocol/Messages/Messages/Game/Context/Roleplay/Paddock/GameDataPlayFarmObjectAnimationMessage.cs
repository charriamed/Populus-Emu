namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameDataPlayFarmObjectAnimationMessage : Message
    {
        public const uint Id = 6026;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort[] CellId { get; set; }

        public GameDataPlayFarmObjectAnimationMessage(ushort[] cellId)
        {
            this.CellId = cellId;
        }

        public GameDataPlayFarmObjectAnimationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)CellId.Count());
            for (var cellIdIndex = 0; cellIdIndex < CellId.Count(); cellIdIndex++)
            {
                writer.WriteVarUShort(CellId[cellIdIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var cellIdCount = reader.ReadUShort();
            CellId = new ushort[cellIdCount];
            for (var cellIdIndex = 0; cellIdIndex < cellIdCount; cellIdIndex++)
            {
                CellId[cellIdIndex] = reader.ReadVarUShort();
            }
        }

    }
}
