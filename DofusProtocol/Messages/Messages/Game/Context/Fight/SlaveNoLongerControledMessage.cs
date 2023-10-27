namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SlaveNoLongerControledMessage : Message
    {
        public const uint Id = 6807;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double MasterId { get; set; }
        public double SlaveId { get; set; }

        public SlaveNoLongerControledMessage(double masterId, double slaveId)
        {
            this.MasterId = masterId;
            this.SlaveId = slaveId;
        }

        public SlaveNoLongerControledMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(MasterId);
            writer.WriteDouble(SlaveId);
        }

        public override void Deserialize(IDataReader reader)
        {
            MasterId = reader.ReadDouble();
            SlaveId = reader.ReadDouble();
        }

    }
}
