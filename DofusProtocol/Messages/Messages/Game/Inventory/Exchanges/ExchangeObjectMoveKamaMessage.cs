namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeObjectMoveKamaMessage : Message
    {
        public const uint Id = 5520;
        public override uint MessageId
        {
            get { return Id; }
        }
        public long Quantity { get; set; }

        public ExchangeObjectMoveKamaMessage(long quantity)
        {
            this.Quantity = quantity;
        }

        public ExchangeObjectMoveKamaMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarLong(Quantity);
        }

        public override void Deserialize(IDataReader reader)
        {
            Quantity = reader.ReadVarLong();
        }

    }
}
