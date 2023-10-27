namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HouseSellingUpdateMessage : Message
    {
        public const uint Id = 6727;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint HouseId { get; set; }
        public int InstanceId { get; set; }
        public bool SecondHand { get; set; }
        public ulong RealPrice { get; set; }
        public string BuyerName { get; set; }

        public HouseSellingUpdateMessage(uint houseId, int instanceId, bool secondHand, ulong realPrice, string buyerName)
        {
            this.HouseId = houseId;
            this.InstanceId = instanceId;
            this.SecondHand = secondHand;
            this.RealPrice = realPrice;
            this.BuyerName = buyerName;
        }

        public HouseSellingUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(HouseId);
            writer.WriteInt(InstanceId);
            writer.WriteBoolean(SecondHand);
            writer.WriteVarULong(RealPrice);
            writer.WriteUTF(BuyerName);
        }

        public override void Deserialize(IDataReader reader)
        {
            HouseId = reader.ReadVarUInt();
            InstanceId = reader.ReadInt();
            SecondHand = reader.ReadBoolean();
            RealPrice = reader.ReadVarULong();
            BuyerName = reader.ReadUTF();
        }

    }
}
