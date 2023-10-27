namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MonsterInGroupInformations : MonsterInGroupLightInformations
    {
        public new const short Id = 144;
        public override short TypeId
        {
            get { return Id; }
        }
        public EntityLook Look { get; set; }

        public MonsterInGroupInformations(int genericId, sbyte grade, short level, EntityLook look)
        {
            this.GenericId = genericId;
            this.Grade = grade;
            this.Level = level;
            this.Look = look;
        }

        public MonsterInGroupInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            Look.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Look = new EntityLook();
            Look.Deserialize(reader);
        }

    }
}
