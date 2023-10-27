namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeRequestedTradeMessage : ExchangeRequestedMessage
    {
        public new const uint Id = 5523;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong Source { get; set; }
        public ulong Target { get; set; }

        public ExchangeRequestedTradeMessage(sbyte exchangeType, ulong source, ulong target)
        {
            this.ExchangeType = exchangeType;
            this.Source = source;
            this.Target = target;
        }

        public ExchangeRequestedTradeMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(Source);
            writer.WriteVarULong(Target);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Source = reader.ReadVarULong();
            Target = reader.ReadVarULong();
        }

    }
}
