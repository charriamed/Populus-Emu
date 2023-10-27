namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildInvitedMessage : Message
    {
        public const uint Id = 5552;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong RecruterId { get; set; }
        public string RecruterName { get; set; }
        public BasicGuildInformations GuildInfo { get; set; }

        public GuildInvitedMessage(ulong recruterId, string recruterName, BasicGuildInformations guildInfo)
        {
            this.RecruterId = recruterId;
            this.RecruterName = recruterName;
            this.GuildInfo = guildInfo;
        }

        public GuildInvitedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(RecruterId);
            writer.WriteUTF(RecruterName);
            GuildInfo.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            RecruterId = reader.ReadVarULong();
            RecruterName = reader.ReadUTF();
            GuildInfo = new BasicGuildInformations();
            GuildInfo.Deserialize(reader);
        }

    }
}
