namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HouseToSellFilterMessage : Message
    {
        public const uint Id = 6137;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int AreaId { get; set; }
        public sbyte AtLeastNbRoom { get; set; }
        public sbyte AtLeastNbChest { get; set; }
        public ushort SkillRequested { get; set; }
        public ulong MaxPrice { get; set; }
        public sbyte OrderBy { get; set; }

        public HouseToSellFilterMessage(int areaId, sbyte atLeastNbRoom, sbyte atLeastNbChest, ushort skillRequested, ulong maxPrice, sbyte orderBy)
        {
            this.AreaId = areaId;
            this.AtLeastNbRoom = atLeastNbRoom;
            this.AtLeastNbChest = atLeastNbChest;
            this.SkillRequested = skillRequested;
            this.MaxPrice = maxPrice;
            this.OrderBy = orderBy;
        }

        public HouseToSellFilterMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(AreaId);
            writer.WriteSByte(AtLeastNbRoom);
            writer.WriteSByte(AtLeastNbChest);
            writer.WriteVarUShort(SkillRequested);
            writer.WriteVarULong(MaxPrice);
            writer.WriteSByte(OrderBy);
        }

        public override void Deserialize(IDataReader reader)
        {
            AreaId = reader.ReadInt();
            AtLeastNbRoom = reader.ReadSByte();
            AtLeastNbChest = reader.ReadSByte();
            SkillRequested = reader.ReadVarUShort();
            MaxPrice = reader.ReadVarULong();
            OrderBy = reader.ReadSByte();
        }

    }
}
