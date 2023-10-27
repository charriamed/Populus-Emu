namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeBidPriceMessage : Message
    {
        public const uint Id = 5755;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort GenericId { get; set; }
        public long AveragePrice { get; set; }

        public ExchangeBidPriceMessage(ushort genericId, long averagePrice)
        {
            this.GenericId = genericId;
            this.AveragePrice = averagePrice;
        }

        public ExchangeBidPriceMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(GenericId);
            writer.WriteVarLong(AveragePrice);
        }

        public override void Deserialize(IDataReader reader)
        {
            GenericId = reader.ReadVarUShort();
            AveragePrice = reader.ReadVarLong();
        }

    }
}
