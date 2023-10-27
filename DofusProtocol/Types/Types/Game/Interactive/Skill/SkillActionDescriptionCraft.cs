namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SkillActionDescriptionCraft : SkillActionDescription
    {
        public new const short Id = 100;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte Probability { get; set; }

        public SkillActionDescriptionCraft(ushort skillId, sbyte probability)
        {
            this.SkillId = skillId;
            this.Probability = probability;
        }

        public SkillActionDescriptionCraft() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(Probability);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Probability = reader.ReadSByte();
        }

    }
}
