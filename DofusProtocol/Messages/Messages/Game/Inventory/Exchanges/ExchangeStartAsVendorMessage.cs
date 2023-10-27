namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeStartAsVendorMessage : Message
    {
        public const uint Id = 5775;
        public override uint MessageId
        {
            get { return Id; }
        }

        public ExchangeStartAsVendorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
        }

        public override void Deserialize(IDataReader reader)
        {
        }

    }
}
