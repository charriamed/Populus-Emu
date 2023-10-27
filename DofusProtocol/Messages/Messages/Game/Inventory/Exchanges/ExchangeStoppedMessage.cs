namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeStoppedMessage : Message
    {
        public const uint Id = 6589;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong ObjectId { get; set; }

        public ExchangeStoppedMessage(ulong objectId)
        {
            this.ObjectId = objectId;
        }

        public ExchangeStoppedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(ObjectId);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadVarULong();
        }

    }
}
