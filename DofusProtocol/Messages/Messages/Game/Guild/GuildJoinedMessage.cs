namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildJoinedMessage : Message
    {
        public const uint Id = 5564;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GuildInformations GuildInfo { get; set; }
        public uint MemberRights { get; set; }

        public GuildJoinedMessage(GuildInformations guildInfo, uint memberRights)
        {
            this.GuildInfo = guildInfo;
            this.MemberRights = memberRights;
        }

        public GuildJoinedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            GuildInfo.Serialize(writer);
            writer.WriteVarUInt(MemberRights);
        }

        public override void Deserialize(IDataReader reader)
        {
            GuildInfo = new GuildInformations();
            GuildInfo.Deserialize(reader);
            MemberRights = reader.ReadVarUInt();
        }

    }
}
