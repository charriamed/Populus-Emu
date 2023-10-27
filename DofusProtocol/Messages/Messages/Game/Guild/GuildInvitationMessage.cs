namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildInvitationMessage : Message
    {
        public const uint Id = 5551;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong TargetId { get; set; }

        public GuildInvitationMessage(ulong targetId)
        {
            this.TargetId = targetId;
        }

        public GuildInvitationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(TargetId);
        }

        public override void Deserialize(IDataReader reader)
        {
            TargetId = reader.ReadVarULong();
        }

    }
}
