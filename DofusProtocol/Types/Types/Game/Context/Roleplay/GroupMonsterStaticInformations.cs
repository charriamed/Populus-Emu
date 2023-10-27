namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GroupMonsterStaticInformations
    {
        public const short Id  = 140;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public MonsterInGroupLightInformations MainCreatureLightInfos { get; set; }
        public MonsterInGroupInformations[] Underlings { get; set; }

        public GroupMonsterStaticInformations(MonsterInGroupLightInformations mainCreatureLightInfos, MonsterInGroupInformations[] underlings)
        {
            this.MainCreatureLightInfos = mainCreatureLightInfos;
            this.Underlings = underlings;
        }

        public GroupMonsterStaticInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            MainCreatureLightInfos.Serialize(writer);
            writer.WriteShort((short)Underlings.Count());
            for (var underlingsIndex = 0; underlingsIndex < Underlings.Count(); underlingsIndex++)
            {
                var objectToSend = Underlings[underlingsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public virtual void Deserialize(IDataReader reader)
        {
            MainCreatureLightInfos = new MonsterInGroupLightInformations();
            MainCreatureLightInfos.Deserialize(reader);
            var underlingsCount = reader.ReadUShort();
            Underlings = new MonsterInGroupInformations[underlingsCount];
            for (var underlingsIndex = 0; underlingsIndex < underlingsCount; underlingsIndex++)
            {
                var objectToAdd = new MonsterInGroupInformations();
                objectToAdd.Deserialize(reader);
                Underlings[underlingsIndex] = objectToAdd;
            }
        }

    }
}
