namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeSellMessage : Message
    {
        public const uint Id = 5778;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint ObjectToSellId { get; set; }
        public uint Quantity { get; set; }

        public ExchangeSellMessage(uint objectToSellId, uint quantity)
        {
            this.ObjectToSellId = objectToSellId;
            this.Quantity = quantity;
        }

        public ExchangeSellMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ObjectToSellId);
            writer.WriteVarUInt(Quantity);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectToSellId = reader.ReadVarUInt();
            Quantity = reader.ReadVarUInt();
        }

    }
}
