namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HouseInformations
    {
        public const short Id  = 111;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public uint HouseId { get; set; }
        public ushort ModelId { get; set; }

        public HouseInformations(uint houseId, ushort modelId)
        {
            this.HouseId = houseId;
            this.ModelId = modelId;
        }

        public HouseInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(HouseId);
            writer.WriteVarUShort(ModelId);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            HouseId = reader.ReadVarUInt();
            ModelId = reader.ReadVarUShort();
        }

    }
}
