namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class JobCrafterDirectoryAddMessage : Message
    {
        public const uint Id = 5651;
        public override uint MessageId
        {
            get { return Id; }
        }
        public JobCrafterDirectoryListEntry ListEntry { get; set; }

        public JobCrafterDirectoryAddMessage(JobCrafterDirectoryListEntry listEntry)
        {
            this.ListEntry = listEntry;
        }

        public JobCrafterDirectoryAddMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            ListEntry.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            ListEntry = new JobCrafterDirectoryListEntry();
            ListEntry.Deserialize(reader);
        }

    }
}
