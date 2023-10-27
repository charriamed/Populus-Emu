namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PaddockInformationsForSell
    {
        public const short Id  = 222;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public string GuildOwner { get; set; }
        public short WorldX { get; set; }
        public short WorldY { get; set; }
        public ushort SubAreaId { get; set; }
        public sbyte NbMount { get; set; }
        public sbyte NbObject { get; set; }
        public ulong Price { get; set; }

        public PaddockInformationsForSell(string guildOwner, short worldX, short worldY, ushort subAreaId, sbyte nbMount, sbyte nbObject, ulong price)
        {
            this.GuildOwner = guildOwner;
            this.WorldX = worldX;
            this.WorldY = worldY;
            this.SubAreaId = subAreaId;
            this.NbMount = nbMount;
            this.NbObject = nbObject;
            this.Price = price;
        }

        public PaddockInformationsForSell() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(GuildOwner);
            writer.WriteShort(WorldX);
            writer.WriteShort(WorldY);
            writer.WriteVarUShort(SubAreaId);
            writer.WriteSByte(NbMount);
            writer.WriteSByte(NbObject);
            writer.WriteVarULong(Price);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            GuildOwner = reader.ReadUTF();
            WorldX = reader.ReadShort();
            WorldY = reader.ReadShort();
            SubAreaId = reader.ReadVarUShort();
            NbMount = reader.ReadSByte();
            NbObject = reader.ReadSByte();
            Price = reader.ReadVarULong();
        }

    }
}
