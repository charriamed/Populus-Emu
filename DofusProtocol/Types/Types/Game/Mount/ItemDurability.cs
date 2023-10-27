namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ItemDurability
    {
        public const short Id  = 168;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public short Durability { get; set; }
        public short DurabilityMax { get; set; }

        public ItemDurability(short durability, short durabilityMax)
        {
            this.Durability = durability;
            this.DurabilityMax = durabilityMax;
        }

        public ItemDurability() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort(Durability);
            writer.WriteShort(DurabilityMax);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            Durability = reader.ReadShort();
            DurabilityMax = reader.ReadShort();
        }

    }
}
