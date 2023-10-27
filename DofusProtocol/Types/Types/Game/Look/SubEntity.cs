namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SubEntity
    {
        public const short Id  = 54;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte BindingPointCategory { get; set; }
        public sbyte BindingPointIndex { get; set; }
        public EntityLook SubEntityLook { get; set; }

        public SubEntity(sbyte bindingPointCategory, sbyte bindingPointIndex, EntityLook subEntityLook)
        {
            this.BindingPointCategory = bindingPointCategory;
            this.BindingPointIndex = bindingPointIndex;
            this.SubEntityLook = subEntityLook;
        }

        public SubEntity() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(BindingPointCategory);
            writer.WriteSByte(BindingPointIndex);
            SubEntityLook.Serialize(writer);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            BindingPointCategory = reader.ReadSByte();
            BindingPointIndex = reader.ReadSByte();
            SubEntityLook = new EntityLook();
            SubEntityLook.Deserialize(reader);
        }

    }
}
