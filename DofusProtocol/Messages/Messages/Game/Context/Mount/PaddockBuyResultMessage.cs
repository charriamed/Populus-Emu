namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PaddockBuyResultMessage : Message
    {
        public const uint Id = 6516;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double PaddockId { get; set; }
        public bool Bought { get; set; }
        public ulong RealPrice { get; set; }

        public PaddockBuyResultMessage(double paddockId, bool bought, ulong realPrice)
        {
            this.PaddockId = paddockId;
            this.Bought = bought;
            this.RealPrice = realPrice;
        }

        public PaddockBuyResultMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(PaddockId);
            writer.WriteBoolean(Bought);
            writer.WriteVarULong(RealPrice);
        }

        public override void Deserialize(IDataReader reader)
        {
            PaddockId = reader.ReadDouble();
            Bought = reader.ReadBoolean();
            RealPrice = reader.ReadVarULong();
        }

    }
}
