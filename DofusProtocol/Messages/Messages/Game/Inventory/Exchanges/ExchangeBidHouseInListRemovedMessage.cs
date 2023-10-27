namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeBidHouseInListRemovedMessage : Message
    {
        public const uint Id = 5950;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int ItemUID { get; set; }

        public ExchangeBidHouseInListRemovedMessage(int itemUID)
        {
            this.ItemUID = itemUID;
        }

        public ExchangeBidHouseInListRemovedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(ItemUID);
        }

        public override void Deserialize(IDataReader reader)
        {
            ItemUID = reader.ReadInt();
        }

    }
}
