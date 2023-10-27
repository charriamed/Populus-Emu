namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ChallengeTargetsListMessage : Message
    {
        public const uint Id = 5613;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double[] TargetIds { get; set; }
        public short[] TargetCells { get; set; }

        public ChallengeTargetsListMessage(double[] targetIds, short[] targetCells)
        {
            this.TargetIds = targetIds;
            this.TargetCells = targetCells;
        }

        public ChallengeTargetsListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)TargetIds.Count());
            for (var targetIdsIndex = 0; targetIdsIndex < TargetIds.Count(); targetIdsIndex++)
            {
                writer.WriteDouble(TargetIds[targetIdsIndex]);
            }
            writer.WriteShort((short)TargetCells.Count());
            for (var targetCellsIndex = 0; targetCellsIndex < TargetCells.Count(); targetCellsIndex++)
            {
                writer.WriteShort(TargetCells[targetCellsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var targetIdsCount = reader.ReadUShort();
            TargetIds = new double[targetIdsCount];
            for (var targetIdsIndex = 0; targetIdsIndex < targetIdsCount; targetIdsIndex++)
            {
                TargetIds[targetIdsIndex] = reader.ReadDouble();
            }
            var targetCellsCount = reader.ReadUShort();
            TargetCells = new short[targetCellsCount];
            for (var targetCellsIndex = 0; targetCellsIndex < targetCellsCount; targetCellsIndex++)
            {
                TargetCells[targetCellsIndex] = reader.ReadShort();
            }
        }

    }
}
