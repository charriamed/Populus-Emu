namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class JobCrafterDirectorySettingsMessage : Message
    {
        public const uint Id = 5652;
        public override uint MessageId
        {
            get { return Id; }
        }
        public JobCrafterDirectorySettings[] CraftersSettings { get; set; }

        public JobCrafterDirectorySettingsMessage(JobCrafterDirectorySettings[] craftersSettings)
        {
            this.CraftersSettings = craftersSettings;
        }

        public JobCrafterDirectorySettingsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)CraftersSettings.Count());
            for (var craftersSettingsIndex = 0; craftersSettingsIndex < CraftersSettings.Count(); craftersSettingsIndex++)
            {
                var objectToSend = CraftersSettings[craftersSettingsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var craftersSettingsCount = reader.ReadUShort();
            CraftersSettings = new JobCrafterDirectorySettings[craftersSettingsCount];
            for (var craftersSettingsIndex = 0; craftersSettingsIndex < craftersSettingsCount; craftersSettingsIndex++)
            {
                var objectToAdd = new JobCrafterDirectorySettings();
                objectToAdd.Deserialize(reader);
                CraftersSettings[craftersSettingsIndex] = objectToAdd;
            }
        }

    }
}
