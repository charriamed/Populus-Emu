namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ContactLookRequestByIdMessage : ContactLookRequestMessage
    {
        public new const uint Id = 5935;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong PlayerId { get; set; }

        public ContactLookRequestByIdMessage(byte requestId, sbyte contactType, ulong playerId)
        {
            this.RequestId = requestId;
            this.ContactType = contactType;
            this.PlayerId = playerId;
        }

        public ContactLookRequestByIdMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(PlayerId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PlayerId = reader.ReadVarULong();
        }

    }
}
