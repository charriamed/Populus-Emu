namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangePlayerRequestMessage : ExchangeRequestMessage
    {
        public new const uint Id = 5773;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong Target { get; set; }

        public ExchangePlayerRequestMessage(sbyte exchangeType, ulong target)
        {
            this.ExchangeType = exchangeType;
            this.Target = target;
        }

        public ExchangePlayerRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(Target);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Target = reader.ReadVarULong();
        }

    }
}
