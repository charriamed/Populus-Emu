namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HumanOptionSkillUse : HumanOption
    {
        public new const short Id = 495;
        public override short TypeId
        {
            get { return Id; }
        }
        public uint ElementId { get; set; }
        public ushort SkillId { get; set; }
        public double SkillEndTime { get; set; }

        public HumanOptionSkillUse(uint elementId, ushort skillId, double skillEndTime)
        {
            this.ElementId = elementId;
            this.SkillId = skillId;
            this.SkillEndTime = skillEndTime;
        }

        public HumanOptionSkillUse() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(ElementId);
            writer.WriteVarUShort(SkillId);
            writer.WriteDouble(SkillEndTime);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ElementId = reader.ReadVarUInt();
            SkillId = reader.ReadVarUShort();
            SkillEndTime = reader.ReadDouble();
        }

    }
}
