namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class InteractiveElementSkill
    {
        public const short Id  = 219;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public uint SkillId { get; set; }
        public int SkillInstanceUid { get; set; }

        public InteractiveElementSkill(uint skillId, int skillInstanceUid)
        {
            this.SkillId = skillId;
            this.SkillInstanceUid = skillInstanceUid;
        }

        public InteractiveElementSkill() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(SkillId);
            writer.WriteInt(SkillInstanceUid);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            SkillId = reader.ReadVarUInt();
            SkillInstanceUid = reader.ReadInt();
        }

    }
}
