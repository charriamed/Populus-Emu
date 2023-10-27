namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeBidHouseItemRemoveOkMessage : Message
    {
        public const uint Id = 5946;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int SellerId { get; set; }

        public ExchangeBidHouseItemRemoveOkMessage(int sellerId)
        {
            this.SellerId = sellerId;
        }

        public ExchangeBidHouseItemRemoveOkMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(SellerId);
        }

        public override void Deserialize(IDataReader reader)
        {
            SellerId = reader.ReadInt();
        }

    }
}
