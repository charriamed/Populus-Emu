namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class InteractiveElementNamedSkill : InteractiveElementSkill
    {
        public new const short Id = 220;
        public override short TypeId
        {
            get { return Id; }
        }
        public uint NameId { get; set; }

        public InteractiveElementNamedSkill(uint skillId, int skillInstanceUid, uint nameId)
        {
            this.SkillId = skillId;
            this.SkillInstanceUid = skillInstanceUid;
            this.NameId = nameId;
        }

        public InteractiveElementNamedSkill() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(NameId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            NameId = reader.ReadVarUInt();
        }

    }
}
