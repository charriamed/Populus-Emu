namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class JobDescription
    {
        public const short Id  = 101;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte JobId { get; set; }
        public SkillActionDescription[] Skills { get; set; }

        public JobDescription(sbyte jobId, SkillActionDescription[] skills)
        {
            this.JobId = jobId;
            this.Skills = skills;
        }

        public JobDescription() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(JobId);
            writer.WriteShort((short)Skills.Count());
            for (var skillsIndex = 0; skillsIndex < Skills.Count(); skillsIndex++)
            {
                var objectToSend = Skills[skillsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public virtual void Deserialize(IDataReader reader)
        {
            JobId = reader.ReadSByte();
            var skillsCount = reader.ReadUShort();
            Skills = new SkillActionDescription[skillsCount];
            for (var skillsIndex = 0; skillsIndex < skillsCount; skillsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<SkillActionDescription>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Skills[skillsIndex] = objectToAdd;
            }
        }

    }
}
