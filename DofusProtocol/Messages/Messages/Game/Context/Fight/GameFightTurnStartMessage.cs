namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightTurnStartMessage : Message
    {
        public const uint Id = 714;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double ObjectId { get; set; }
        public uint WaitTime { get; set; }

        public GameFightTurnStartMessage(double objectId, uint waitTime)
        {
            this.ObjectId = objectId;
            this.WaitTime = waitTime;
        }

        public GameFightTurnStartMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(ObjectId);
            writer.WriteVarUInt(WaitTime);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadDouble();
            WaitTime = reader.ReadVarUInt();
        }

    }
}
