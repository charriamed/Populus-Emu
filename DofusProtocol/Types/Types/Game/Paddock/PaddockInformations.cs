namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PaddockInformations
    {
        public const short Id  = 132;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort MaxOutdoorMount { get; set; }
        public ushort MaxItems { get; set; }

        public PaddockInformations(ushort maxOutdoorMount, ushort maxItems)
        {
            this.MaxOutdoorMount = maxOutdoorMount;
            this.MaxItems = maxItems;
        }

        public PaddockInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(MaxOutdoorMount);
            writer.WriteVarUShort(MaxItems);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            MaxOutdoorMount = reader.ReadVarUShort();
            MaxItems = reader.ReadVarUShort();
        }

    }
}
