namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MountInformationRequestMessage : Message
    {
        public const uint Id = 5972;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double ObjectId { get; set; }
        public double Time { get; set; }

        public MountInformationRequestMessage(double objectId, double time)
        {
            this.ObjectId = objectId;
            this.Time = time;
        }

        public MountInformationRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(ObjectId);
            writer.WriteDouble(Time);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadDouble();
            Time = reader.ReadDouble();
        }

    }
}
