namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceInvitationMessage : Message
    {
        public const uint Id = 6395;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong TargetId { get; set; }

        public AllianceInvitationMessage(ulong targetId)
        {
            this.TargetId = targetId;
        }

        public AllianceInvitationMessage() { }

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
