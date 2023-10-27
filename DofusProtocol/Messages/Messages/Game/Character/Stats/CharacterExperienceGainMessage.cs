namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterExperienceGainMessage : Message
    {
        public const uint Id = 6321;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong ExperienceCharacter { get; set; }
        public ulong ExperienceMount { get; set; }
        public ulong ExperienceGuild { get; set; }
        public ulong ExperienceIncarnation { get; set; }

        public CharacterExperienceGainMessage(ulong experienceCharacter, ulong experienceMount, ulong experienceGuild, ulong experienceIncarnation)
        {
            this.ExperienceCharacter = experienceCharacter;
            this.ExperienceMount = experienceMount;
            this.ExperienceGuild = experienceGuild;
            this.ExperienceIncarnation = experienceIncarnation;
        }

        public CharacterExperienceGainMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(ExperienceCharacter);
            writer.WriteVarULong(ExperienceMount);
            writer.WriteVarULong(ExperienceGuild);
            writer.WriteVarULong(ExperienceIncarnation);
        }

        public override void Deserialize(IDataReader reader)
        {
            ExperienceCharacter = reader.ReadVarULong();
            ExperienceMount = reader.ReadVarULong();
            ExperienceGuild = reader.ReadVarULong();
            ExperienceIncarnation = reader.ReadVarULong();
        }

    }
}
