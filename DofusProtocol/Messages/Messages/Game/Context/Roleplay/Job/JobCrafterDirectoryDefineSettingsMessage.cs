namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class JobCrafterDirectoryDefineSettingsMessage : Message
    {
        public const uint Id = 5649;
        public override uint MessageId
        {
            get { return Id; }
        }
        public JobCrafterDirectorySettings Settings { get; set; }

        public JobCrafterDirectoryDefineSettingsMessage(JobCrafterDirectorySettings settings)
        {
            this.Settings = settings;
        }

        public JobCrafterDirectoryDefineSettingsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Settings.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Settings = new JobCrafterDirectorySettings();
            Settings.Deserialize(reader);
        }

    }
}
