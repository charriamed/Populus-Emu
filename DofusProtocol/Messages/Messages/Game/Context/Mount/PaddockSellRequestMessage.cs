namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PaddockSellRequestMessage : Message
    {
        public const uint Id = 5953;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong Price { get; set; }
        public bool ForSale { get; set; }

        public PaddockSellRequestMessage(ulong price, bool forSale)
        {
            this.Price = price;
            this.ForSale = forSale;
        }

        public PaddockSellRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(Price);
            writer.WriteBoolean(ForSale);
        }

        public override void Deserialize(IDataReader reader)
        {
            Price = reader.ReadVarULong();
            ForSale = reader.ReadBoolean();
        }

    }
}
