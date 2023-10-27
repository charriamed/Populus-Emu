namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class BreachReward
    {
        public const short Id  = 559;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public uint ObjectId { get; set; }
        public byte[] BuyLocks { get; set; }
        public string BuyCriterion { get; set; }
        public bool Bought { get; set; }
        public uint Price { get; set; }

        public BreachReward(uint objectId, byte[] buyLocks, string buyCriterion, bool bought, uint price)
        {
            this.ObjectId = objectId;
            this.BuyLocks = buyLocks;
            this.BuyCriterion = buyCriterion;
            this.Bought = bought;
            this.Price = price;
        }

        public BreachReward() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ObjectId);
            writer.WriteShort((short)BuyLocks.Count());
            for (var buyLocksIndex = 0; buyLocksIndex < BuyLocks.Count(); buyLocksIndex++)
            {
                writer.WriteByte(BuyLocks[buyLocksIndex]);
            }
            writer.WriteUTF(BuyCriterion);
            writer.WriteBoolean(Bought);
            writer.WriteVarUInt(Price);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadVarUInt();
            var buyLocksCount = reader.ReadUShort();
            BuyLocks = new byte[buyLocksCount];
            for (var buyLocksIndex = 0; buyLocksIndex < buyLocksCount; buyLocksIndex++)
            {
                BuyLocks[buyLocksIndex] = reader.ReadByte();
            }
            BuyCriterion = reader.ReadUTF();
            Bought = reader.ReadBoolean();
            Price = reader.ReadVarUInt();
        }

    }
}
