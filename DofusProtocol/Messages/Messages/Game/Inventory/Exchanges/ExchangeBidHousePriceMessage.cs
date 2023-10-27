namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeBidHousePriceMessage : Message
    {
        public const uint Id = 5805;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort GenId { get; set; }

        public ExchangeBidHousePriceMessage(ushort genId)
        {
            this.GenId = genId;
        }

        public ExchangeBidHousePriceMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(GenId);
        }

        public override void Deserialize(IDataReader reader)
        {
            GenId = reader.ReadVarUShort();
        }

    }
}
