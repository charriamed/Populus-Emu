namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildFactsRequestMessage : Message
    {
        public const uint Id = 6404;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint GuildId { get; set; }

        public GuildFactsRequestMessage(uint guildId)
        {
            this.GuildId = guildId;
        }

        public GuildFactsRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(GuildId);
        }

        public override void Deserialize(IDataReader reader)
        {
            GuildId = reader.ReadVarUInt();
        }

    }
}
