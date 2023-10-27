namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterHardcoreOrEpicInformations : CharacterBaseInformations
    {
        public new const short Id = 474;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte DeathState { get; set; }
        public ushort DeathCount { get; set; }
        public ushort DeathMaxLevel { get; set; }

        public CharacterHardcoreOrEpicInformations(ulong objectId, string name, ushort level, EntityLook entityLook, sbyte breed, bool sex, sbyte deathState, ushort deathCount, ushort deathMaxLevel)
        {
            this.ObjectId = objectId;
            this.Name = name;
            this.Level = level;
            this.EntityLook = entityLook;
            this.Breed = breed;
            this.Sex = sex;
            this.DeathState = deathState;
            this.DeathCount = deathCount;
            this.DeathMaxLevel = deathMaxLevel;
        }

        public CharacterHardcoreOrEpicInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(DeathState);
            writer.WriteVarUShort(DeathCount);
            writer.WriteVarUShort(DeathMaxLevel);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            DeathState = reader.ReadSByte();
            DeathCount = reader.ReadVarUShort();
            DeathMaxLevel = reader.ReadVarUShort();
        }

    }
}
