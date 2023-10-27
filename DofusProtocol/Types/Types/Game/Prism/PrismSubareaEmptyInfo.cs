namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PrismSubareaEmptyInfo
    {
        public const short Id  = 438;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort SubAreaId { get; set; }
        public uint AllianceId { get; set; }

        public PrismSubareaEmptyInfo(ushort subAreaId, uint allianceId)
        {
            this.SubAreaId = subAreaId;
            this.AllianceId = allianceId;
        }

        public PrismSubareaEmptyInfo() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SubAreaId);
            writer.WriteVarUInt(AllianceId);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            SubAreaId = reader.ReadVarUShort();
            AllianceId = reader.ReadVarUInt();
        }

    }
}
