namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class JobExperience
    {
        public const short Id  = 98;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte JobId { get; set; }
        public byte JobLevel { get; set; }
        public ulong JobXP { get; set; }
        public ulong JobXpLevelFloor { get; set; }
        public ulong JobXpNextLevelFloor { get; set; }

        public JobExperience(sbyte jobId, byte jobLevel, ulong jobXP, ulong jobXpLevelFloor, ulong jobXpNextLevelFloor)
        {
            this.JobId = jobId;
            this.JobLevel = jobLevel;
            this.JobXP = jobXP;
            this.JobXpLevelFloor = jobXpLevelFloor;
            this.JobXpNextLevelFloor = jobXpNextLevelFloor;
        }

        public JobExperience() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(JobId);
            writer.WriteByte(JobLevel);
            writer.WriteVarULong(JobXP);
            writer.WriteVarULong(JobXpLevelFloor);
            writer.WriteVarULong(JobXpNextLevelFloor);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            JobId = reader.ReadSByte();
            JobLevel = reader.ReadByte();
            JobXP = reader.ReadVarULong();
            JobXpLevelFloor = reader.ReadVarULong();
            JobXpNextLevelFloor = reader.ReadVarULong();
        }

    }
}
