namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PaddockBuyableInformations
    {
        public const short Id  = 130;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ulong Price { get; set; }
        public bool Locked { get; set; }

        public PaddockBuyableInformations(ulong price, bool locked)
        {
            this.Price = price;
            this.Locked = locked;
        }

        public PaddockBuyableInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(Price);
            writer.WriteBoolean(Locked);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            Price = reader.ReadVarULong();
            Locked = reader.ReadBoolean();
        }

    }
}
