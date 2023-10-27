namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class JobCrafterDirectoryListEntry
    {
        public const short Id  = 196;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public JobCrafterDirectoryEntryPlayerInfo PlayerInfo { get; set; }
        public JobCrafterDirectoryEntryJobInfo JobInfo { get; set; }

        public JobCrafterDirectoryListEntry(JobCrafterDirectoryEntryPlayerInfo playerInfo, JobCrafterDirectoryEntryJobInfo jobInfo)
        {
            this.PlayerInfo = playerInfo;
            this.JobInfo = jobInfo;
        }

        public JobCrafterDirectoryListEntry() { }

        public virtual void Serialize(IDataWriter writer)
        {
            PlayerInfo.Serialize(writer);
            JobInfo.Serialize(writer);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            PlayerInfo = new JobCrafterDirectoryEntryPlayerInfo();
            PlayerInfo.Deserialize(reader);
            JobInfo = new JobCrafterDirectoryEntryJobInfo();
            JobInfo.Deserialize(reader);
        }

    }
}
