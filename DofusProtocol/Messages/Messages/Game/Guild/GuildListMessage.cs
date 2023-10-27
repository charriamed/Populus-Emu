namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GuildListMessage : Message
    {
        public const uint Id = 6413;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GuildInformations[] Guilds { get; set; }

        public GuildListMessage(GuildInformations[] guilds)
        {
            this.Guilds = guilds;
        }

        public GuildListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Guilds.Count());
            for (var guildsIndex = 0; guildsIndex < Guilds.Count(); guildsIndex++)
            {
                var objectToSend = Guilds[guildsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var guildsCount = reader.ReadUShort();
            Guilds = new GuildInformations[guildsCount];
            for (var guildsIndex = 0; guildsIndex < guildsCount; guildsIndex++)
            {
                var objectToAdd = new GuildInformations();
                objectToAdd.Deserialize(reader);
                Guilds[guildsIndex] = objectToAdd;
            }
        }

    }
}
