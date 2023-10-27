namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class JobDescriptionMessage : Message
    {
        public const uint Id = 5655;
        public override uint MessageId
        {
            get { return Id; }
        }
        public JobDescription[] JobsDescription { get; set; }

        public JobDescriptionMessage(JobDescription[] jobsDescription)
        {
            this.JobsDescription = jobsDescription;
        }

        public JobDescriptionMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)JobsDescription.Count());
            for (var jobsDescriptionIndex = 0; jobsDescriptionIndex < JobsDescription.Count(); jobsDescriptionIndex++)
            {
                var objectToSend = JobsDescription[jobsDescriptionIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var jobsDescriptionCount = reader.ReadUShort();
            JobsDescription = new JobDescription[jobsDescriptionCount];
            for (var jobsDescriptionIndex = 0; jobsDescriptionIndex < jobsDescriptionCount; jobsDescriptionIndex++)
            {
                var objectToAdd = new JobDescription();
                objectToAdd.Deserialize(reader);
                JobsDescription[jobsDescriptionIndex] = objectToAdd;
            }
        }

    }
}
