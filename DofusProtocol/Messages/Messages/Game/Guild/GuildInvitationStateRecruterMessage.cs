namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildInvitationStateRecruterMessage : Message
    {
        public const uint Id = 5563;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string RecrutedName { get; set; }
        public sbyte InvitationState { get; set; }

        public GuildInvitationStateRecruterMessage(string recrutedName, sbyte invitationState)
        {
            this.RecrutedName = recrutedName;
            this.InvitationState = invitationState;
        }

        public GuildInvitationStateRecruterMessage() { }

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
