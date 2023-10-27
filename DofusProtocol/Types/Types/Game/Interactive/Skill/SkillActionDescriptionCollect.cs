namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SkillActionDescriptionCollect : SkillActionDescriptionTimed
    {
        public new const short Id = 99;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort Min { get; set; }
        public ushort Max { get; set; }

        public SkillActionDescriptionCollect(ushort skillId, byte time, ushort min, ushort max)
        {
            this.SkillId = skillId;
            this.Time = time;
            this.Min = min;
            this.Max = max;
        }

        public SkillActionDescriptionCollect() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(Min);
            writer.WriteVarUShort(Max);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Min = reader.ReadVarUShort();
            Max = reader.ReadVarUShort();
        }

    }
}
