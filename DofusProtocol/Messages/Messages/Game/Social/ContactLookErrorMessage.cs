namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ContactLookErrorMessage : Message
    {
        public const uint Id = 6045;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint RequestId { get; set; }

        public ContactLookErrorMessage(uint requestId)
        {
            this.RequestId = requestId;
        }

        public ContactLookErrorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(RequestId);
        }

        public override void Deserialize(IDataReader reader)
        {
            RequestId = reader.ReadVarUInt();
        }

    }
}
