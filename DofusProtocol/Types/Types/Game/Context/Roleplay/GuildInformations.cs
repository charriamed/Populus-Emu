namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildInformations : BasicGuildInformations
    {
        public new const short Id = 127;
        public override short TypeId
        {
            get { return Id; }
        }
        public GuildEmblem GuildEmblem { get; set; }

        public GuildInformations(uint guildId, string guildName, byte guildLevel, GuildEmblem guildEmblem)
        {
            this.GuildId = guildId;
            this.GuildName = guildName;
            this.GuildLevel = guildLevel;
            this.GuildEmblem = guildEmblem;
        }

        public GuildInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            GuildEmblem.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            GuildEmblem = new GuildEmblem();
            GuildEmblem.Deserialize(reader);
        }

    }
}
