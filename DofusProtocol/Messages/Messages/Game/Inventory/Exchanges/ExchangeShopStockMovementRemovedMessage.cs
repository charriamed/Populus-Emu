namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeShopStockMovementRemovedMessage : Message
    {
        public const uint Id = 5907;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint ObjectId { get; set; }

        public ExchangeShopStockMovementRemovedMessage(uint objectId)
        {
            this.ObjectId = objectId;
        }

        public ExchangeShopStockMovementRemovedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ObjectId);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadVarUInt();
        }

    }
}
