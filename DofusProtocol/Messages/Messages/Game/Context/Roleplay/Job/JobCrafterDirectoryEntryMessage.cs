namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class JobCrafterDirectoryEntryMessage : Message
    {
        public const uint Id = 6044;
        public override uint MessageId
        {
            get { return Id; }
        }
        public JobCrafterDirectoryEntryPlayerInfo PlayerInfo { get; set; }
        public JobCrafterDirectoryEntryJobInfo[] JobInfoList { get; set; }
        public EntityLook PlayerLook { get; set; }

        public JobCrafterDirectoryEntryMessage(JobCrafterDirectoryEntryPlayerInfo playerInfo, JobCrafterDirectoryEntryJobInfo[] jobInfoList, EntityLook playerLook)
        {
            this.PlayerInfo = playerInfo;
            this.JobInfoList = jobInfoList;
            this.PlayerLook = playerLook;
        }

        public JobCrafterDirectoryEntryMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            PlayerInfo.Serialize(writer);
            writer.WriteShort((short)JobInfoList.Count());
            for (var jobInfoListIndex = 0; jobInfoListIndex < JobInfoList.Count(); jobInfoListIndex++)
            {
                var objectToSend = JobInfoList[jobInfoListIndex];
                objectToSend.Serialize(writer);
            }
            PlayerLook.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            PlayerInfo = new JobCrafterDirectoryEntryPlayerInfo();
            PlayerInfo.Deserialize(reader);
            var jobInfoListCount = reader.ReadUShort();
            JobInfoList = new JobCrafterDirectoryEntryJobInfo[jobInfoListCount];
            for (var jobInfoListIndex = 0; jobInfoListIndex < jobInfoListCount; jobInfoListIndex++)
            {
                var objectToAdd = new JobCrafterDirectoryEntryJobInfo();
                objectToAdd.Deserialize(reader);
                JobInfoList[jobInfoListIndex] = objectToAdd;
            }
            PlayerLook = new EntityLook();
            PlayerLook.Deserialize(reader);
        }

    }
}
