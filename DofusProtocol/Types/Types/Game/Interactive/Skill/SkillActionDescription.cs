namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SkillActionDescription
    {
        public const short Id  = 102;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort SkillId { get; set; }

        public SkillActionDescription(ushort skillId)
        {
            this.SkillId = skillId;
        }

        public SkillActionDescription() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SkillId);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            SkillId = reader.ReadVarUShort();
        }

    }
}
