namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeStartOkRecycleTradeMessage : Message
    {
        public const uint Id = 6600;
        public override uint MessageId
        {
            get { return Id; }
        }
        public short PercentToPrism { get; set; }
        public short PercentToPlayer { get; set; }

        public ExchangeStartOkRecycleTradeMessage(short percentToPrism, short percentToPlayer)
        {
            this.PercentToPrism = percentToPrism;
            this.PercentToPlayer = percentToPlayer;
        }

        public ExchangeStartOkRecycleTradeMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(PercentToPrism);
            writer.WriteShort(PercentToPlayer);
        }

        public override void Deserialize(IDataReader reader)
        {
            PercentToPrism = reader.ReadShort();
            PercentToPlayer = reader.ReadShort();
        }

    }
}
