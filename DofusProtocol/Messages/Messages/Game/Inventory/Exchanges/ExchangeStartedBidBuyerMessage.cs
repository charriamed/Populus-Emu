namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeStartedBidBuyerMessage : Message
    {
        public const uint Id = 5904;
        public override uint MessageId
        {
            get { return Id; }
        }
        public SellerBuyerDescriptor BuyerDescriptor { get; set; }

        public ExchangeStartedBidBuyerMessage(SellerBuyerDescriptor buyerDescriptor)
        {
            this.BuyerDescriptor = buyerDescriptor;
        }

        public ExchangeStartedBidBuyerMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            BuyerDescriptor.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            BuyerDescriptor = new SellerBuyerDescriptor();
            BuyerDescriptor.Deserialize(reader);
        }

    }
}
