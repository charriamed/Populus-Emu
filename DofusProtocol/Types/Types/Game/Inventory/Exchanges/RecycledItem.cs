namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class RecycledItem
    {
        public const short Id  = 547;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort ObjectId { get; set; }
        public uint Qty { get; set; }

        public RecycledItem(ushort objectId, uint qty)
        {
            this.ObjectId = objectId;
            this.Qty = qty;
        }

        public RecycledItem() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ObjectId);
            writer.WriteUInt(Qty);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadVarUShort();
            Qty = reader.ReadUInt();
        }

    }
}
