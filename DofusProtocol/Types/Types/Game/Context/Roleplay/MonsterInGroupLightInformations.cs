namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MonsterInGroupLightInformations
    {
        public const short Id  = 395;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public int GenericId { get; set; }
        public sbyte Grade { get; set; }
        public short Level { get; set; }

        public MonsterInGroupLightInformations(int genericId, sbyte grade, short level)
        {
            this.GenericId = genericId;
            this.Grade = grade;
            this.Level = level;
        }

        public MonsterInGroupLightInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt(GenericId);
            writer.WriteSByte(Grade);
            writer.WriteShort(Level);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            GenericId = reader.ReadInt();
            Grade = reader.ReadSByte();
            Level = reader.ReadShort();
        }

    }
}
