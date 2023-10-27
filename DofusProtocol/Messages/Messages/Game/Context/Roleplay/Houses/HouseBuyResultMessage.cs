namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HouseBuyResultMessage : Message
    {
        public const uint Id = 5735;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool SecondHand { get; set; }
        public bool Bought { get; set; }
        public uint HouseId { get; set; }
        public int InstanceId { get; set; }
        public ulong RealPrice { get; set; }

        public HouseBuyResultMessage(bool secondHand, bool bought, uint houseId, int instanceId, ulong realPrice)
        {
            this.SecondHand = secondHand;
            this.Bought = bought;
            this.HouseId = houseId;
            this.InstanceId = instanceId;
            this.RealPrice = realPrice;
        }

        public HouseBuyResultMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, SecondHand);
            flag = BooleanByteWrapper.SetFlag(flag, 1, Bought);
            writer.WriteByte(flag);
            writer.WriteVarUInt(HouseId);
            writer.WriteInt(InstanceId);
            writer.WriteVarULong(RealPrice);
        }

        public override void Deserialize(IDataReader reader)
        {
            var flag = reader.ReadByte();
            SecondHand = BooleanByteWrapper.GetFlag(flag, 0);
            Bought = BooleanByteWrapper.GetFlag(flag, 1);
            HouseId = reader.ReadVarUInt();
            InstanceId = reader.ReadInt();
            RealPrice = reader.ReadVarULong();
        }

    }
}
