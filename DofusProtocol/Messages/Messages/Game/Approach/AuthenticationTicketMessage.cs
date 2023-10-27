namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AuthenticationTicketMessage : Message
    {
        public const uint Id = 110;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Lang { get; set; }
        public string Ticket { get; set; }

        public AuthenticationTicketMessage(string lang, string ticket)
        {
            this.Lang = lang;
            this.Ticket = ticket;
        }

        public AuthenticationTicketMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Lang);
            writer.WriteUTF(Ticket);
        }

        public override void Deserialize(IDataReader reader)
        {
            Lang = reader.ReadUTF();
            Ticket = reader.ReadUTF();
        }

    }
}
