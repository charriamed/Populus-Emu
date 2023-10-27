namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class JobCrafterDirectoryListRequestMessage : Message
    {
        public const uint Id = 6047;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte JobId { get; set; }

        public JobCrafterDirectoryListRequestMessage(sbyte jobId)
        {
            this.JobId = jobId;
        }

        public JobCrafterDirectoryListRequestMessage() { }

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
