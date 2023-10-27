namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class JobCrafterDirectoryListMessage : Message
    {
        public const uint Id = 6046;
        public override uint MessageId
        {
            get { return Id; }
        }
        public JobCrafterDirectoryListEntry[] ListEntries { get; set; }

        public JobCrafterDirectoryListMessage(JobCrafterDirectoryListEntry[] listEntries)
        {
            this.ListEntries = listEntries;
        }

        public JobCrafterDirectoryListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)ListEntries.Count());
            for (var listEntriesIndex = 0; listEntriesIndex < ListEntries.Count(); listEntriesIndex++)
            {
                var objectToSend = ListEntries[listEntriesIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var listEntriesCount = reader.ReadUShort();
            ListEntries = new JobCrafterDirectoryListEntry[listEntriesCount];
            for (var listEntriesIndex = 0; listEntriesIndex < listEntriesCount; listEntriesIndex++)
            {
                var objectToAdd = new JobCrafterDirectoryListEntry();
                objectToAdd.Deserialize(reader);
                ListEntries[listEntriesIndex] = objectToAdd;
            }
        }

    }
}
