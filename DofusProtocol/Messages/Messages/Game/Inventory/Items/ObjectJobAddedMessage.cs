namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectJobAddedMessage : Message
    {
        public const uint Id = 6014;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte JobId { get; set; }

        public ObjectJobAddedMessage(sbyte jobId)
        {
            this.JobId = jobId;
        }

        public ObjectJobAddedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(JobId);
        }

        public override void Deserialize(IDataReader reader)
        {
            JobId = reader.ReadSByte();
        }

    }
}
