namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildModificationValidMessage : Message
    {
        public const uint Id = 6323;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string GuildName { get; set; }
        public GuildEmblem GuildEmblem { get; set; }

        public GuildModificationValidMessage(string guildName, GuildEmblem guildEmblem)
        {
            this.GuildName = guildName;
            this.GuildEmblem = guildEmblem;
        }

        public GuildModificationValidMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(GuildName);
            GuildEmblem.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            GuildName = reader.ReadUTF();
            GuildEmblem = new GuildEmblem();
            GuildEmblem.Deserialize(reader);
        }

    }
}
