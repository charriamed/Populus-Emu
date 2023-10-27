namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeBidHouseListMessage : Message
    {
        public const uint Id = 5807;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort ObjectId { get; set; }

        public ExchangeBidHouseListMessage(ushort objectId)
        {
            this.ObjectId = objectId;
        }

        public ExchangeBidHouseListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ObjectId);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadVarUShort();
        }

    }
}
