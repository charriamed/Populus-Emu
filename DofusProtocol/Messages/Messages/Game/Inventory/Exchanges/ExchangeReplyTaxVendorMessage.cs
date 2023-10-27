namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeReplyTaxVendorMessage : Message
    {
        public const uint Id = 5787;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong ObjectValue { get; set; }
        public ulong TotalTaxValue { get; set; }

        public ExchangeReplyTaxVendorMessage(ulong objectValue, ulong totalTaxValue)
        {
            this.ObjectValue = objectValue;
            this.TotalTaxValue = totalTaxValue;
        }

        public ExchangeReplyTaxVendorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(ObjectValue);
            writer.WriteVarULong(TotalTaxValue);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectValue = reader.ReadVarULong();
            TotalTaxValue = reader.ReadVarULong();
        }

    }
}
