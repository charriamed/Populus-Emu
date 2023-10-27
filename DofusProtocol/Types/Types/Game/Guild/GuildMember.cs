namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildMember : CharacterMinimalInformations
    {
        public new const short Id = 88;
        public override short TypeId
        {
            get { return Id; }
        }
        public bool Sex { get; set; }
        public bool HavenBagShared { get; set; }
        public sbyte Breed { get; set; }
        public ushort Rank { get; set; }
        public ulong GivenExperience { get; set; }
        public sbyte ExperienceGivenPercent { get; set; }
        public uint Rights { get; set; }
        public sbyte Connected { get; set; }
        public sbyte AlignmentSide { get; set; }
        public ushort HoursSinceLastConnection { get; set; }
        public ushort MoodSmileyId { get; set; }
        public int AccountId { get; set; }
        public int AchievementPoints { get; set; }
        public PlayerStatus Status { get; set; }

        public GuildMember(ulong objectId, string name, ushort level, bool sex, bool havenBagShared, sbyte breed, ushort rank, ulong givenExperience, sbyte experienceGivenPercent, uint rights, sbyte connected, sbyte alignmentSide, ushort hoursSinceLastConnection, ushort moodSmileyId, int accountId, int achievementPoints, PlayerStatus status)
        {
            this.ObjectId = objectId;
            this.Name = name;
            this.Level = level;
            this.Sex = sex;
            this.HavenBagShared = havenBagShared;
            this.Breed = breed;
            this.Rank = rank;
            this.GivenExperience = givenExperience;
            this.ExperienceGivenPercent = experienceGivenPercent;
            this.Rights = rights;
            this.Connected = connected;
            this.AlignmentSide = alignmentSide;
            this.HoursSinceLastConnection = hoursSinceLastConnection;
            this.MoodSmileyId = moodSmileyId;
            this.AccountId = accountId;
            this.AchievementPoints = achievementPoints;
            this.Status = status;
        }

        public GuildMember() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, Sex);
            flag = BooleanByteWrapper.SetFlag(flag, 1, HavenBagShared);
            writer.WriteByte(flag);
            writer.WriteSByte(Breed);
            writer.WriteVarUShort(Rank);
            writer.WriteVarULong(GivenExperience);
            writer.WriteSByte(ExperienceGivenPercent);
            writer.WriteVarUInt(Rights);
            writer.WriteSByte(Connected);
            writer.WriteSByte(AlignmentSide);
            writer.WriteUShort(HoursSinceLastConnection);
            writer.WriteVarUShort(MoodSmileyId);
            writer.WriteInt(AccountId);
            writer.WriteInt(AchievementPoints);
            writer.WriteShort(Status.TypeId);
            Status.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var flag = reader.ReadByte();
            Sex = BooleanByteWrapper.GetFlag(flag, 0);
            HavenBagShared = BooleanByteWrapper.GetFlag(flag, 1);
            Breed = reader.ReadSByte();
            Rank = reader.ReadVarUShort();
            GivenExperience = reader.ReadVarULong();
            ExperienceGivenPercent = reader.ReadSByte();
            Rights = reader.ReadVarUInt();
            Connected = reader.ReadSByte();
            AlignmentSide = reader.ReadSByte();
            HoursSinceLastConnection = reader.ReadUShort();
            MoodSmileyId = reader.ReadVarUShort();
            AccountId = reader.ReadInt();
            AchievementPoints = reader.ReadInt();
            Status = ProtocolTypeManager.GetInstance<PlayerStatus>(reader.ReadShort());
            Status.Deserialize(reader);
        }

    }
}
