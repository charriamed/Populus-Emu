namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildFactsErrorMessage : Message
    {
        public const uint Id = 6424;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint GuildId { get; set; }

        public GuildFactsErrorMessage(uint guildId)
        {
            this.GuildId = guildId;
        }

        public GuildFactsErrorMessage() { }

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
