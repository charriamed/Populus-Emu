namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ServerExperienceModificatorMessage : Message
    {
        public const uint Id = 6237;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort ExperiencePercent { get; set; }

        public ServerExperienceModificatorMessage(ushort experiencePercent)
        {
            this.ExperiencePercent = experiencePercent;
        }

        public ServerExperienceModificatorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ExperiencePercent);
        }

        public override void Deserialize(IDataReader reader)
        {
            ExperiencePercent = reader.ReadVarUShort();
        }

    }
}
