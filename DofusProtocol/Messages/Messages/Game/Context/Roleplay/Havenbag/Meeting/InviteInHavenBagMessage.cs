namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class InviteInHavenBagMessage : Message
    {
        public const uint Id = 6642;
        public override uint MessageId
        {
            get { return Id; }
        }
        public CharacterMinimalInformations GuestInformations { get; set; }
        public bool Accept { get; set; }

        public InviteInHavenBagMessage(CharacterMinimalInformations guestInformations, bool accept)
        {
            this.GuestInformations = guestInformations;
            this.Accept = accept;
        }

        public InviteInHavenBagMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            GuestInformations.Serialize(writer);
            writer.WriteBoolean(Accept);
        }

        public override void Deserialize(IDataReader reader)
        {
            GuestInformations = new CharacterMinimalInformations();
            GuestInformations.Deserialize(reader);
            Accept = reader.ReadBoolean();
        }

    }
}
