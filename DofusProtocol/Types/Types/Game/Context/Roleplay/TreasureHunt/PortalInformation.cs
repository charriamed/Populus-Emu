namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PortalInformation
    {
        public const short Id  = 466;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public int PortalId { get; set; }
        public short AreaId { get; set; }

        public PortalInformation(int portalId, short areaId)
        {
            this.PortalId = portalId;
            this.AreaId = areaId;
        }

        public PortalInformation() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt(PortalId);
            writer.WriteShort(AreaId);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            PortalId = reader.ReadInt();
            AreaId = reader.ReadShort();
        }

    }
}
