namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class FinishMoveListMessage : Message
    {
        public const uint Id = 6704;
        public override uint MessageId
        {
            get { return Id; }
        }
        public FinishMoveInformations[] FinishMoves { get; set; }

        public FinishMoveListMessage(FinishMoveInformations[] finishMoves)
        {
            this.FinishMoves = finishMoves;
        }

        public FinishMoveListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)FinishMoves.Count());
            for (var finishMovesIndex = 0; finishMovesIndex < FinishMoves.Count(); finishMovesIndex++)
            {
                var objectToSend = FinishMoves[finishMovesIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var finishMovesCount = reader.ReadUShort();
            FinishMoves = new FinishMoveInformations[finishMovesCount];
            for (var finishMovesIndex = 0; finishMovesIndex < finishMovesCount; finishMovesIndex++)
            {
                var objectToAdd = new FinishMoveInformations();
                objectToAdd.Deserialize(reader);
                FinishMoves[finishMovesIndex] = objectToAdd;
            }
        }

    }
}
