namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FinishMoveInformations
    {
        public const short Id  = 506;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public int FinishMoveId { get; set; }
        public bool FinishMoveState { get; set; }

        public FinishMoveInformations(int finishMoveId, bool finishMoveState)
        {
            this.FinishMoveId = finishMoveId;
            this.FinishMoveState = finishMoveState;
        }

        public FinishMoveInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt(FinishMoveId);
            writer.WriteBoolean(FinishMoveState);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            FinishMoveId = reader.ReadInt();
            FinishMoveState = reader.ReadBoolean();
        }

    }
}
