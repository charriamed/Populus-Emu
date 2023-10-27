namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FriendSpouseInformations
    {
        public const short Id  = 77;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public int SpouseAccountId { get; set; }
        public ulong SpouseId { get; set; }
        public string SpouseName { get; set; }
        public ushort SpouseLevel { get; set; }
        public sbyte Breed { get; set; }
        public sbyte Sex { get; set; }
        public EntityLook SpouseEntityLook { get; set; }
        public GuildInformations GuildInfo { get; set; }
        public sbyte AlignmentSide { get; set; }

        public FriendSpouseInformations(int spouseAccountId, ulong spouseId, string spouseName, ushort spouseLevel, sbyte breed, sbyte sex, EntityLook spouseEntityLook, GuildInformations guildInfo, sbyte alignmentSide)
        {
            this.SpouseAccountId = spouseAccountId;
            this.SpouseId = spouseId;
            this.SpouseName = spouseName;
            this.SpouseLevel = spouseLevel;
            this.Breed = breed;
            this.Sex = sex;
            this.SpouseEntityLook = spouseEntityLook;
            this.GuildInfo = guildInfo;
            this.AlignmentSide = alignmentSide;
        }

        public FriendSpouseInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt(SpouseAccountId);
            writer.WriteVarULong(SpouseId);
            writer.WriteUTF(SpouseName);
            writer.WriteVarUShort(SpouseLevel);
            writer.WriteSByte(Breed);
            writer.WriteSByte(Sex);
            SpouseEntityLook.Serialize(writer);
            GuildInfo.Serialize(writer);
            writer.WriteSByte(AlignmentSide);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            SpouseAccountId = reader.ReadInt();
            SpouseId = reader.ReadVarULong();
            SpouseName = reader.ReadUTF();
            SpouseLevel = reader.ReadVarUShort();
            Breed = reader.ReadSByte();
            Sex = reader.ReadSByte();
            SpouseEntityLook = new EntityLook();
            SpouseEntityLook.Deserialize(reader);
            GuildInfo = new GuildInformations();
            GuildInfo.Deserialize(reader);
            AlignmentSide = reader.ReadSByte();
        }

    }
}
