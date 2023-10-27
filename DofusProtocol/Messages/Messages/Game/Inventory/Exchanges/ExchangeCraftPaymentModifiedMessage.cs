namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeCraftPaymentModifiedMessage : Message
    {
        public const uint Id = 6578;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong GoldSum { get; set; }

        public ExchangeCraftPaymentModifiedMessage(ulong goldSum)
        {
            this.GoldSum = goldSum;
        }

        public ExchangeCraftPaymentModifiedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(GoldSum);
        }

        public override void Deserialize(IDataReader reader)
        {
            GoldSum = reader.ReadVarULong();
        }

    }
}
