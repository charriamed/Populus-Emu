namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class InviteInHavenBagClosedMessage : Message
    {
        public const uint Id = 6645;
        public override uint MessageId
        {
            get { return Id; }
        }
        public CharacterMinimalInformations HostInformations { get; set; }

        public InviteInHavenBagClosedMessage(CharacterMinimalInformations hostInformations)
        {
            this.HostInformations = hostInformations;
        }

        public InviteInHavenBagClosedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            HostInformations.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            HostInformations = new CharacterMinimalInformations();
            HostInformations.Deserialize(reader);
        }

    }
}
