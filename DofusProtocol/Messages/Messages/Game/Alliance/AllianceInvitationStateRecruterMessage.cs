namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceInvitationStateRecruterMessage : Message
    {
        public const uint Id = 6396;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string RecrutedName { get; set; }
        public sbyte InvitationState { get; set; }

        public AllianceInvitationStateRecruterMessage(string recrutedName, sbyte invitationState)
        {
            this.RecrutedName = recrutedName;
            this.InvitationState = invitationState;
        }

        public AllianceInvitationStateRecruterMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(RecrutedName);
            writer.WriteSByte(InvitationState);
        }

        public override void Deserialize(IDataReader reader)
        {
            RecrutedName = reader.ReadUTF();
            InvitationState = reader.ReadSByte();
        }

    }
}
