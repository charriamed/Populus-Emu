namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceChangeGuildRightsMessage : Message
    {
        public const uint Id = 6426;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint GuildId { get; set; }
        public sbyte Rights { get; set; }

        public AllianceChangeGuildRightsMessage(uint guildId, sbyte rights)
        {
            this.GuildId = guildId;
            this.Rights = rights;
        }

        public AllianceChangeGuildRightsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(GuildId);
            writer.WriteSByte(Rights);
        }

        public override void Deserialize(IDataReader reader)
        {
            GuildId = reader.ReadVarUInt();
            Rights = reader.ReadSByte();
        }

    }
}
