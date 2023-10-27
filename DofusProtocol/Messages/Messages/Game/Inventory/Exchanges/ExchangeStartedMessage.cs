namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeStartedMessage : Message
    {
        public const uint Id = 5512;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte ExchangeType { get; set; }

        public ExchangeStartedMessage(sbyte exchangeType)
        {
            this.ExchangeType = exchangeType;
        }

        public ExchangeStartedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(ExchangeType);
        }

        public override void Deserialize(IDataReader reader)
        {
            ExchangeType = reader.ReadSByte();
        }

    }
}
