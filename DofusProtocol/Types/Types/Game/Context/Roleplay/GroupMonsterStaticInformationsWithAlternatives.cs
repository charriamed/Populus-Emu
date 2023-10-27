namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GroupMonsterStaticInformationsWithAlternatives : GroupMonsterStaticInformations
    {
        public new const short Id = 396;
        public override short TypeId
        {
            get { return Id; }
        }
        public AlternativeMonstersInGroupLightInformations[] Alternatives { get; set; }

        public GroupMonsterStaticInformationsWithAlternatives(MonsterInGroupLightInformations mainCreatureLightInfos, MonsterInGroupInformations[] underlings, AlternativeMonstersInGroupLightInformations[] alternatives)
        {
            this.MainCreatureLightInfos = mainCreatureLightInfos;
            this.Underlings = underlings;
            this.Alternatives = alternatives;
        }

        public GroupMonsterStaticInformationsWithAlternatives() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)Alternatives.Count());
            for (var alternativesIndex = 0; alternativesIndex < Alternatives.Count(); alternativesIndex++)
            {
                var objectToSend = Alternatives[alternativesIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var alternativesCount = reader.ReadUShort();
            Alternatives = new AlternativeMonstersInGroupLightInformations[alternativesCount];
            for (var alternativesIndex = 0; alternativesIndex < alternativesCount; alternativesIndex++)
            {
                var objectToAdd = new AlternativeMonstersInGroupLightInformations();
                objectToAdd.Deserialize(reader);
                Alternatives[alternativesIndex] = objectToAdd;
            }
        }

    }
}
