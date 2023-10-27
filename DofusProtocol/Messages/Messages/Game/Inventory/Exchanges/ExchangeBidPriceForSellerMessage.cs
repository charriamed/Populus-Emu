namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeBidPriceForSellerMessage : ExchangeBidPriceMessage
    {
        public new const uint Id = 6464;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool AllIdentical { get; set; }
        public ulong[] MinimalPrices { get; set; }

        public ExchangeBidPriceForSellerMessage(ushort genericId, long averagePrice, bool allIdentical, ulong[] minimalPrices)
        {
            this.GenericId = genericId;
            this.AveragePrice = averagePrice;
            this.AllIdentical = allIdentical;
            this.MinimalPrices = minimalPrices;
        }

        public ExchangeBidPriceForSellerMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(AllIdentical);
            writer.WriteShort((short)MinimalPrices.Count());
            for (var minimalPricesIndex = 0; minimalPricesIndex < MinimalPrices.Count(); minimalPricesIndex++)
            {
                writer.WriteVarULong(MinimalPrices[minimalPricesIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            AllIdentical = reader.ReadBoolean();
            var minimalPricesCount = reader.ReadUShort();
            MinimalPrices = new ulong[minimalPricesCount];
            for (var minimalPricesIndex = 0; minimalPricesIndex < minimalPricesCount; minimalPricesIndex++)
            {
                MinimalPrices[minimalPricesIndex] = reader.ReadVarULong();
            }
        }

    }
}
