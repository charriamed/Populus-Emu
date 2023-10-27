namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildInvitationStateRecrutedMessage : Message
    {
        public const uint Id = 5548;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte InvitationState { get; set; }

        public GuildInvitationStateRecrutedMessage(sbyte invitationState)
        {
            this.InvitationState = invitationState;
        }

        public GuildInvitationStateRecrutedMessage() { }

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
