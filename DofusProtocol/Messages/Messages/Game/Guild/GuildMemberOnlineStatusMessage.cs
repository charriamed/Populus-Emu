namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildMemberOnlineStatusMessage : Message
    {
        public const uint Id = 6061;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong MemberId { get; set; }
        public bool Online { get; set; }

        public GuildMemberOnlineStatusMessage(ulong memberId, bool online)
        {
            this.MemberId = memberId;
            this.Online = online;
        }

        public GuildMemberOnlineStatusMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(MemberId);
            writer.WriteBoolean(Online);
        }

        public override void Deserialize(IDataReader reader)
        {
            MemberId = reader.ReadVarULong();
            Online = reader.ReadBoolean();
        }

    }
}
