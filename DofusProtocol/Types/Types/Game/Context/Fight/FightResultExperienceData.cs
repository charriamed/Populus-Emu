namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightResultExperienceData : FightResultAdditionalData
    {
        public new const short Id = 192;
        public override short TypeId
        {
            get { return Id; }
        }
        public bool ShowExperience { get; set; }
        public bool ShowExperienceLevelFloor { get; set; }
        public bool ShowExperienceNextLevelFloor { get; set; }
        public bool ShowExperienceFightDelta { get; set; }
        public bool ShowExperienceForGuild { get; set; }
        public bool ShowExperienceForMount { get; set; }
        public bool IsIncarnationExperience { get; set; }
        public ulong Experience { get; set; }
        public ulong ExperienceLevelFloor { get; set; }
        public ulong ExperienceNextLevelFloor { get; set; }
        public ulong ExperienceFightDelta { get; set; }
        public ulong ExperienceForGuild { get; set; }
        public ulong ExperienceForMount { get; set; }
        public sbyte RerollExperienceMul { get; set; }

        public FightResultExperienceData(bool showExperience, bool showExperienceLevelFloor, bool showExperienceNextLevelFloor, bool showExperienceFightDelta, bool showExperienceForGuild, bool showExperienceForMount, bool isIncarnationExperience, ulong experience, ulong experienceLevelFloor, ulong experienceNextLevelFloor, ulong experienceFightDelta, ulong experienceForGuild, ulong experienceForMount, sbyte rerollExperienceMul)
        {
            this.ShowExperience = showExperience;
            this.ShowExperienceLevelFloor = showExperienceLevelFloor;
            this.ShowExperienceNextLevelFloor = showExperienceNextLevelFloor;
            this.ShowExperienceFightDelta = showExperienceFightDelta;
            this.ShowExperienceForGuild = showExperienceForGuild;
            this.ShowExperienceForMount = showExperienceForMount;
            this.IsIncarnationExperience = isIncarnationExperience;
            this.Experience = experience;
            this.ExperienceLevelFloor = experienceLevelFloor;
            this.ExperienceNextLevelFloor = experienceNextLevelFloor;
            this.ExperienceFightDelta = experienceFightDelta;
            this.ExperienceForGuild = experienceForGuild;
            this.ExperienceForMount = experienceForMount;
            this.RerollExperienceMul = rerollExperienceMul;
        }

        public FightResultExperienceData() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, ShowExperience);
            flag = BooleanByteWrapper.SetFlag(flag, 1, ShowExperienceLevelFloor);
            flag = BooleanByteWrapper.SetFlag(flag, 2, ShowExperienceNextLevelFloor);
            flag = BooleanByteWrapper.SetFlag(flag, 3, ShowExperienceFightDelta);
            flag = BooleanByteWrapper.SetFlag(flag, 4, ShowExperienceForGuild);
            flag = BooleanByteWrapper.SetFlag(flag, 5, ShowExperienceForMount);
            flag = BooleanByteWrapper.SetFlag(flag, 6, IsIncarnationExperience);
            writer.WriteByte(flag);
            writer.WriteVarULong(Experience);
            writer.WriteVarULong(ExperienceLevelFloor);
            writer.WriteVarULong(ExperienceNextLevelFloor);
            writer.WriteVarULong(ExperienceFightDelta);
            writer.WriteVarULong(ExperienceForGuild);
            writer.WriteVarULong(ExperienceForMount);
            writer.WriteSByte(RerollExperienceMul);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var flag = reader.ReadByte();
            ShowExperience = BooleanByteWrapper.GetFlag(flag, 0);
            ShowExperienceLevelFloor = BooleanByteWrapper.GetFlag(flag, 1);
            ShowExperienceNextLevelFloor = BooleanByteWrapper.GetFlag(flag, 2);
            ShowExperienceFightDelta = BooleanByteWrapper.GetFlag(flag, 3);
            ShowExperienceForGuild = BooleanByteWrapper.GetFlag(flag, 4);
            ShowExperienceForMount = BooleanByteWrapper.GetFlag(flag, 5);
            IsIncarnationExperience = BooleanByteWrapper.GetFlag(flag, 6);
            Experience = reader.ReadVarULong();
            ExperienceLevelFloor = reader.ReadVarULong();
            ExperienceNextLevelFloor = reader.ReadVarULong();
            ExperienceFightDelta = reader.ReadVarULong();
            ExperienceForGuild = reader.ReadVarULong();
            ExperienceForMount = reader.ReadVarULong();
            RerollExperienceMul = reader.ReadSByte();
        }

    }
}
