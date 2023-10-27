namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class JobExperienceMultiUpdateMessage : Message
    {
        public const uint Id = 5809;
        public override uint MessageId
        {
            get { return Id; }
        }
        public JobExperience[] ExperiencesUpdate { get; set; }

        public JobExperienceMultiUpdateMessage(JobExperience[] experiencesUpdate)
        {
            this.ExperiencesUpdate = experiencesUpdate;
        }

        public JobExperienceMultiUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)ExperiencesUpdate.Count());
            for (var experiencesUpdateIndex = 0; experiencesUpdateIndex < ExperiencesUpdate.Count(); experiencesUpdateIndex++)
            {
                var objectToSend = ExperiencesUpdate[experiencesUpdateIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var experiencesUpdateCount = reader.ReadUShort();
            ExperiencesUpdate = new JobExperience[experiencesUpdateCount];
            for (var experiencesUpdateIndex = 0; experiencesUpdateIndex < experiencesUpdateCount; experiencesUpdateIndex++)
            {
                var objectToAdd = new JobExperience();
                objectToAdd.Deserialize(reader);
                ExperiencesUpdate[experiencesUpdateIndex] = objectToAdd;
            }
        }

    }
}
