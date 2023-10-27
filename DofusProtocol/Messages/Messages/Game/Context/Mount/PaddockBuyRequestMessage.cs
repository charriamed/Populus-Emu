namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PaddockBuyRequestMessage : Message
    {
        public const uint Id = 5951;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong ProposedPrice { get; set; }

        public PaddockBuyRequestMessage(ulong proposedPrice)
        {
            this.ProposedPrice = proposedPrice;
        }

        public PaddockBuyRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(ProposedPrice);
        }

        public override void Deserialize(IDataReader reader)
        {
            ProposedPrice = reader.ReadVarULong();
        }

    }
}
