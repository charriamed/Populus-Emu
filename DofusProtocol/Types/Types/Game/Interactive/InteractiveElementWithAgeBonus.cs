namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class InteractiveElementWithAgeBonus : InteractiveElement
    {
        public new const short Id = 398;
        public override short TypeId
        {
            get { return Id; }
        }
        public short AgeBonus { get; set; }

        public InteractiveElementWithAgeBonus(int elementId, int elementTypeId, InteractiveElementSkill[] enabledSkills, InteractiveElementSkill[] disabledSkills, bool onCurrentMap, short ageBonus)
        {
            this.ElementId = elementId;
            this.ElementTypeId = elementTypeId;
            this.EnabledSkills = enabledSkills;
            this.DisabledSkills = disabledSkills;
            this.OnCurrentMap = onCurrentMap;
            this.AgeBonus = ageBonus;
        }

        public InteractiveElementWithAgeBonus() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(AgeBonus);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            AgeBonus = reader.ReadShort();
        }

    }
}
