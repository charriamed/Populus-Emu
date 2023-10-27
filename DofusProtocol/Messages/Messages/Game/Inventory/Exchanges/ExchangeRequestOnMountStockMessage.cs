namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeRequestOnMountStockMessage : Message
    {
        public const uint Id = 5986;
        public override uint MessageId
        {
            get { return Id; }
        }

        public ExchangeRequestOnMountStockMessage() { }

        public override void Serialize(IDataWriter writer)
        {
        }

        public override void Deserialize(IDataReader reader)
        {
        }

    }
}
