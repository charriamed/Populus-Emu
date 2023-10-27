namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceInvitationStateRecrutedMessage : Message
    {
        public const uint Id = 6392;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte InvitationState { get; set; }

        public AllianceInvitationStateRecrutedMessage(sbyte invitationState)
        {
            this.InvitationState = invitationState;
        }

        public AllianceInvitationStateRecrutedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(InvitationState);
        }

        public override void Deserialize(IDataReader reader)
        {
            InvitationState = reader.ReadSByte();
        }

    }
}
