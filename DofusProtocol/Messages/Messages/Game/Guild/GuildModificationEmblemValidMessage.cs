namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildModificationEmblemValidMessage : Message
    {
        public const uint Id = 6328;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GuildEmblem GuildEmblem { get; set; }

        public GuildModificationEmblemValidMessage(GuildEmblem guildEmblem)
        {
            this.GuildEmblem = guildEmblem;
        }

        public GuildModificationEmblemValidMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            GuildEmblem.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            GuildEmblem = new GuildEmblem();
            GuildEmblem.Deserialize(reader);
        }

    }
}
