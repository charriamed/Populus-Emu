namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class IndexedEntityLook
    {
        public const short Id  = 405;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public EntityLook Look { get; set; }
        public sbyte Index { get; set; }

        public IndexedEntityLook(EntityLook look, sbyte index)
        {
            this.Look = look;
            this.Index = index;
        }

        public IndexedEntityLook() { }

        public virtual void Serialize(IDataWriter writer)
        {
            Look.Serialize(writer);
            writer.WriteSByte(Index);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            Look = new EntityLook();
            Look.Deserialize(reader);
            Index = reader.ReadSByte();
        }

    }
}
