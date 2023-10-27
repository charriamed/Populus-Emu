namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeCraftPaymentModificationRequestMessage : Message
    {
        public const uint Id = 6579;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong Quantity { get; set; }

        public ExchangeCraftPaymentModificationRequestMessage(ulong quantity)
        {
            this.Quantity = quantity;
        }

        public ExchangeCraftPaymentModificationRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(Quantity);
        }

        public override void Deserialize(IDataReader reader)
        {
            Quantity = reader.ReadVarULong();
        }

    }
}
