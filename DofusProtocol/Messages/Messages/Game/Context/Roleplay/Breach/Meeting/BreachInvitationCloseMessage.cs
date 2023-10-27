namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BreachInvitationCloseMessage : Message
    {
        public const uint Id = 6790;
        public override uint MessageId
        {
            get { return Id; }
        }
        public CharacterMinimalInformations Host { get; set; }

        public BreachInvitationCloseMessage(CharacterMinimalInformations host)
        {
            this.Host = host;
        }

        public BreachInvitationCloseMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Host.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Host = new CharacterMinimalInformations();
            Host.Deserialize(reader);
        }

    }
}
