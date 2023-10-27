namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class JobCrafterDirectoryEntryJobInfo
    {
        public const short Id  = 195;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte JobId { get; set; }
        public byte JobLevel { get; set; }
        public bool Free { get; set; }
        public byte MinLevel { get; set; }

        public JobCrafterDirectoryEntryJobInfo(sbyte jobId, byte jobLevel, bool free, byte minLevel)
        {
            this.JobId = jobId;
            this.JobLevel = jobLevel;
            this.Free = free;
            this.MinLevel = minLevel;
        }

        public JobCrafterDirectoryEntryJobInfo() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(JobId);
            writer.WriteByte(JobLevel);
            writer.WriteBoolean(Free);
            writer.WriteByte(MinLevel);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            JobId = reader.ReadSByte();
            JobLevel = reader.ReadByte();
            Free = reader.ReadBoolean();
            MinLevel = reader.ReadByte();
        }

    }
}
