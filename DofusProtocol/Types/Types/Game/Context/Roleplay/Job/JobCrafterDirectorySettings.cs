namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class JobCrafterDirectorySettings
    {
        public const short Id  = 97;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte JobId { get; set; }
        public byte MinLevel { get; set; }
        public bool Free { get; set; }

        public JobCrafterDirectorySettings(sbyte jobId, byte minLevel, bool free)
        {
            this.JobId = jobId;
            this.MinLevel = minLevel;
            this.Free = free;
        }

        public JobCrafterDirectorySettings() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(JobId);
            writer.WriteByte(MinLevel);
            writer.WriteBoolean(Free);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            JobId = reader.ReadSByte();
            MinLevel = reader.ReadByte();
            Free = reader.ReadBoolean();
        }

    }
}
