namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HouseSellRequestMessage : Message
    {
        public const uint Id = 5697;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int InstanceId { get; set; }
        public ulong Amount { get; set; }
        public bool ForSale { get; set; }

        public HouseSellRequestMessage(int instanceId, ulong amount, bool forSale)
        {
            this.InstanceId = instanceId;
            this.Amount = amount;
            this.ForSale = forSale;
        }

        public HouseSellRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(InstanceId);
            writer.WriteVarULong(Amount);
            writer.WriteBoolean(ForSale);
        }

        public override void Deserialize(IDataReader reader)
        {
            InstanceId = reader.ReadInt();
            Amount = reader.ReadVarULong();
            ForSale = reader.ReadBoolean();
        }

    }
}
