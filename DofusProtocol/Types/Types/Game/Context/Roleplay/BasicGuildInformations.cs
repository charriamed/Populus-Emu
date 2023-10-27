namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BasicGuildInformations : AbstractSocialGroupInfos
    {
        public new const short Id = 365;
        public override short TypeId
        {
            get { return Id; }
        }
        public uint GuildId { get; set; }
        public string GuildName { get; set; }
        public byte GuildLevel { get; set; }

        public BasicGuildInformations(uint guildId, string guildName, byte guildLevel)
        {
            this.GuildId = guildId;
            this.GuildName = guildName;
            this.GuildLevel = guildLevel;
        }

        public BasicGuildInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(GuildId);
            writer.WriteUTF(GuildName);
            writer.WriteByte(GuildLevel);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            GuildId = reader.ReadVarUInt();
            GuildName = reader.ReadUTF();
            GuildLevel = reader.ReadByte();
        }

    }
}
