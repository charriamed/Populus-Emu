namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyEntityBaseInformation
    {
        public const short Id  = 552;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte IndexId { get; set; }
        public sbyte EntityModelId { get; set; }
        public EntityLook EntityLook { get; set; }

        public PartyEntityBaseInformation(sbyte indexId, sbyte entityModelId, EntityLook entityLook)
        {
            this.IndexId = indexId;
            this.EntityModelId = entityModelId;
            this.EntityLook = entityLook;
        }

        public PartyEntityBaseInformation() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(IndexId);
            writer.WriteSByte(EntityModelId);
            EntityLook.Serialize(writer);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            IndexId = reader.ReadSByte();
            EntityModelId = reader.ReadSByte();
            EntityLook = new EntityLook();
            EntityLook.Deserialize(reader);
        }

    }
}
