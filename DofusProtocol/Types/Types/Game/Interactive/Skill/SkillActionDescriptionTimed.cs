namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SkillActionDescriptionTimed : SkillActionDescription
    {
        public new const short Id = 103;
        public override short TypeId
        {
            get { return Id; }
        }
        public byte Time { get; set; }

        public SkillActionDescriptionTimed(ushort skillId, byte time)
        {
            this.SkillId = skillId;
            this.Time = time;
        }

        public SkillActionDescriptionTimed() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte(Time);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Time = reader.ReadByte();
        }

    }
}
