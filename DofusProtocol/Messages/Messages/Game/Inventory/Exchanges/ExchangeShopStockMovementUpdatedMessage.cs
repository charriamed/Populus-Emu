namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeShopStockMovementUpdatedMessage : Message
    {
        public const uint Id = 5909;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectItemToSell ObjectInfo { get; set; }

        public ExchangeShopStockMovementUpdatedMessage(ObjectItemToSell objectInfo)
        {
            this.ObjectInfo = objectInfo;
        }

        public ExchangeShopStockMovementUpdatedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            ObjectInfo.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectInfo = new ObjectItemToSell();
            ObjectInfo.Deserialize(reader);
        }

    }
}
