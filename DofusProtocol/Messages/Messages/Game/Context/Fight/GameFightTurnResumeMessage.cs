namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightTurnResumeMessage : GameFightTurnStartMessage
    {
        public new const uint Id = 6307;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint RemainingTime { get; set; }

        public GameFightTurnResumeMessage(double objectId, uint waitTime, uint remainingTime)
        {
            this.ObjectId = objectId;
            this.WaitTime = waitTime;
            this.RemainingTime = remainingTime;
        }

        public GameFightTurnResumeMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(RemainingTime);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            RemainingTime = reader.ReadVarUInt();
        }

    }
}
