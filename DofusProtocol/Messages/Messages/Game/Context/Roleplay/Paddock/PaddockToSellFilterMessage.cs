namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PaddockToSellFilterMessage : Message
    {
        public const uint Id = 6161;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int AreaId { get; set; }
        public sbyte AtLeastNbMount { get; set; }
        public sbyte AtLeastNbMachine { get; set; }
        public ulong MaxPrice { get; set; }
        public sbyte OrderBy { get; set; }

        public PaddockToSellFilterMessage(int areaId, sbyte atLeastNbMount, sbyte atLeastNbMachine, ulong maxPrice, sbyte orderBy)
        {
            this.AreaId = areaId;
            this.AtLeastNbMount = atLeastNbMount;
            this.AtLeastNbMachine = atLeastNbMachine;
            this.MaxPrice = maxPrice;
            this.OrderBy = orderBy;
        }

        public PaddockToSellFilterMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(AreaId);
            writer.WriteSByte(AtLeastNbMount);
            writer.WriteSByte(AtLeastNbMachine);
            writer.WriteVarULong(MaxPrice);
            writer.WriteSByte(OrderBy);
        }

        public override void Deserialize(IDataReader reader)
        {
            AreaId = reader.ReadInt();
            AtLeastNbMount = reader.ReadSByte();
            AtLeastNbMachine = reader.ReadSByte();
            MaxPrice = reader.ReadVarULong();
            OrderBy = reader.ReadSByte();
        }

    }
}
