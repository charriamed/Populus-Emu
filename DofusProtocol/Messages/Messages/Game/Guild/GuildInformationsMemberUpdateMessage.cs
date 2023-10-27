namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildInformationsMemberUpdateMessage : Message
    {
        public const uint Id = 5597;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GuildMember Member { get; set; }

        public GuildInformationsMemberUpdateMessage(GuildMember member)
        {
            this.Member = member;
        }

        public GuildInformationsMemberUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Member.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Member = new GuildMember();
            Member.Deserialize(reader);
        }

    }
}
