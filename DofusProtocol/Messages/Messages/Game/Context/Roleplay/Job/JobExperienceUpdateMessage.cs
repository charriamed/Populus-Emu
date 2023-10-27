namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class JobExperienceUpdateMessage : Message
    {
        public const uint Id = 5654;
        public override uint MessageId
        {
            get { return Id; }
        }
        public JobExperience ExperiencesUpdate { get; set; }

        public JobExperienceUpdateMessage(JobExperience experiencesUpdate)
        {
            this.ExperiencesUpdate = experiencesUpdate;
        }

        public JobExperienceUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            ExperiencesUpdate.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            ExperiencesUpdate = new JobExperience();
            ExperiencesUpdate.Deserialize(reader);
        }

    }
}
