namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PaddockSellBuyDialogMessage : Message
    {
        public const uint Id = 6018;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Bsell { get; set; }
        public uint OwnerId { get; set; }
        public ulong Price { get; set; }

        public PaddockSellBuyDialogMessage(bool bsell, uint ownerId, ulong price)
        {
            this.Bsell = bsell;
            this.OwnerId = ownerId;
            this.Price = price;
        }

        public PaddockSellBuyDialogMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Bsell);
            writer.WriteVarUInt(OwnerId);
            writer.WriteVarULong(Price);
        }

        public override void Deserialize(IDataReader reader)
        {
            Bsell = reader.ReadBoolean();
            OwnerId = reader.ReadVarUInt();
            Price = reader.ReadVarULong();
        }

    }
}
