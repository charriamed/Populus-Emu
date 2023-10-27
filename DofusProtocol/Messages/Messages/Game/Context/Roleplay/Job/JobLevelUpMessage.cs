namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class JobLevelUpMessage : Message
    {
        public const uint Id = 5656;
        public override uint MessageId
        {
            get { return Id; }
        }
        public byte NewLevel { get; set; }
        public JobDescription JobsDescription { get; set; }

        public JobLevelUpMessage(byte newLevel, JobDescription jobsDescription)
        {
            this.NewLevel = newLevel;
            this.JobsDescription = jobsDescription;
        }

        public JobLevelUpMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(NewLevel);
            JobsDescription.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            NewLevel = reader.ReadByte();
            JobsDescription = new JobDescription();
            JobsDescription.Deserialize(reader);
        }

    }
}
