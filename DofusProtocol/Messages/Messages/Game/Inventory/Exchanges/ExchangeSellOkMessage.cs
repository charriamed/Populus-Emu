namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeSellOkMessage : Message
    {
        public const uint Id = 5792;
        public override uint MessageId
        {
            get { return Id; }
        }

        public ExchangeSellOkMessage() { }

        public override void Serialize(IDataWriter writer)
        {
        }

        public override void Deserialize(IDataReader reader)
        {
        }

    }
}
