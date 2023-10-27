namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildKickRequestMessage : Message
    {
        public const uint Id = 5887;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong KickedId { get; set; }

        public GuildKickRequestMessage(ulong kickedId)
        {
            this.KickedId = kickedId;
        }

        public GuildKickRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(KickedId);
        }

        public override void Deserialize(IDataReader reader)
        {
            KickedId = reader.ReadVarULong();
        }

    }
}
