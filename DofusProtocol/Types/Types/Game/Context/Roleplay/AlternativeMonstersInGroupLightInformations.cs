namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class AlternativeMonstersInGroupLightInformations
    {
        public const short Id  = 394;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public int PlayerCount { get; set; }
        public MonsterInGroupLightInformations[] Monsters { get; set; }

        public AlternativeMonstersInGroupLightInformations(int playerCount, MonsterInGroupLightInformations[] monsters)
        {
            this.PlayerCount = playerCount;
            this.Monsters = monsters;
        }

        public AlternativeMonstersInGroupLightInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt(PlayerCount);
            writer.WriteShort((short)Monsters.Count());
            for (var monstersIndex = 0; monstersIndex < Monsters.Count(); monstersIndex++)
            {
                var objectToSend = Monsters[monstersIndex];
                objectToSend.Serialize(writer);
            }
        }

        public virtual void Deserialize(IDataReader reader)
        {
            PlayerCount = reader.ReadInt();
            var monstersCount = reader.ReadUShort();
            Monsters = new MonsterInGroupLightInformations[monstersCount];
            for (var monstersIndex = 0; monstersIndex < monstersCount; monstersIndex++)
            {
                var objectToAdd = new MonsterInGroupLightInformations();
                objectToAdd.Deserialize(reader);
                Monsters[monstersIndex] = objectToAdd;
            }
        }

    }
}
