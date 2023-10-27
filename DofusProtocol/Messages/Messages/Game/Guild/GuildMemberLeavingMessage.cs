namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildMemberLeavingMessage : Message
    {
        public const uint Id = 5923;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Kicked { get; set; }
        public ulong MemberId { get; set; }

        public GuildMemberLeavingMessage(bool kicked, ulong memberId)
        {
            this.Kicked = kicked;
            this.MemberId = memberId;
        }

        public GuildMemberLeavingMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Kicked);
            writer.WriteVarULong(MemberId);
        }

        public override void Deserialize(IDataReader reader)
        {
            Kicked = reader.ReadBoolean();
            MemberId = reader.ReadVarULong();
        }

    }
}
