namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BreachInvitationResponseMessage : Message
    {
        public const uint Id = 6792;
        public override uint MessageId
        {
            get { return Id; }
        }
        public CharacterMinimalInformations Guest { get; set; }
        public bool Accept { get; set; }

        public BreachInvitationResponseMessage(CharacterMinimalInformations guest, bool accept)
        {
            this.Guest = guest;
            this.Accept = accept;
        }

        public BreachInvitationResponseMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Guest.Serialize(writer);
            writer.WriteBoolean(Accept);
        }

        public override void Deserialize(IDataReader reader)
        {
            Guest = new CharacterMinimalInformations();
            Guest.Deserialize(reader);
            Accept = reader.ReadBoolean();
        }

    }
}
