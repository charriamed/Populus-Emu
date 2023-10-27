namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ContactLookRequestMessage : Message
    {
        public const uint Id = 5932;
        public override uint MessageId
        {
            get { return Id; }
        }
        public byte RequestId { get; set; }
        public sbyte ContactType { get; set; }

        public ContactLookRequestMessage(byte requestId, sbyte contactType)
        {
            this.RequestId = requestId;
            this.ContactType = contactType;
        }

        public ContactLookRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(RequestId);
            writer.WriteSByte(ContactType);
        }

        public override void Deserialize(IDataReader reader)
        {
            RequestId = reader.ReadByte();
            ContactType = reader.ReadSByte();
        }

    }
}
