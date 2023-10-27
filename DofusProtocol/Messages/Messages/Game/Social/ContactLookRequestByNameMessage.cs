namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ContactLookRequestByNameMessage : ContactLookRequestMessage
    {
        public new const uint Id = 5933;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string PlayerName { get; set; }

        public ContactLookRequestByNameMessage(byte requestId, sbyte contactType, string playerName)
        {
            this.RequestId = requestId;
            this.ContactType = contactType;
            this.PlayerName = playerName;
        }

        public ContactLookRequestByNameMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(PlayerName);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PlayerName = reader.ReadUTF();
        }

    }
}
