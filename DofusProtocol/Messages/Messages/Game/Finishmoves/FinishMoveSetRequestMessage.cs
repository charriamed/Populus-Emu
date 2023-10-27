namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FinishMoveSetRequestMessage : Message
    {
        public const uint Id = 6703;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int FinishMoveId { get; set; }
        public bool FinishMoveState { get; set; }

        public FinishMoveSetRequestMessage(int finishMoveId, bool finishMoveState)
        {
            this.FinishMoveId = finishMoveId;
            this.FinishMoveState = finishMoveState;
        }

        public FinishMoveSetRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(FinishMoveId);
            writer.WriteBoolean(FinishMoveState);
        }

        public override void Deserialize(IDataReader reader)
        {
            FinishMoveId = reader.ReadInt();
            FinishMoveState = reader.ReadBoolean();
        }

    }
}
