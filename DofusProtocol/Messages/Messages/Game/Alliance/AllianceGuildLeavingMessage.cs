namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceGuildLeavingMessage : Message
    {
        public const uint Id = 6399;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Kicked { get; set; }
        public uint GuildId { get; set; }

        public AllianceGuildLeavingMessage(bool kicked, uint guildId)
        {
            this.Kicked = kicked;
            this.GuildId = guildId;
        }

        public AllianceGuildLeavingMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Kicked);
            writer.WriteVarUInt(GuildId);
        }

        public override void Deserialize(IDataReader reader)
        {
            Kicked = reader.ReadBoolean();
            GuildId = reader.ReadVarUInt();
        }

    }
}
