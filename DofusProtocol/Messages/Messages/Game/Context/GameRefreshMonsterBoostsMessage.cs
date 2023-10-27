namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameRefreshMonsterBoostsMessage : Message
    {
        public const uint Id = 6618;
        public override uint MessageId
        {
            get { return Id; }
        }
        public MonsterBoosts[] MonsterBoosts { get; set; }
        public MonsterBoosts[] FamilyBoosts { get; set; }

        public GameRefreshMonsterBoostsMessage(MonsterBoosts[] monsterBoosts, MonsterBoosts[] familyBoosts)
        {
            this.MonsterBoosts = monsterBoosts;
            this.FamilyBoosts = familyBoosts;
        }

        public GameRefreshMonsterBoostsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)MonsterBoosts.Count());
            for (var monsterBoostsIndex = 0; monsterBoostsIndex < MonsterBoosts.Count(); monsterBoostsIndex++)
            {
                var objectToSend = MonsterBoosts[monsterBoostsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)FamilyBoosts.Count());
            for (var familyBoostsIndex = 0; familyBoostsIndex < FamilyBoosts.Count(); familyBoostsIndex++)
            {
                var objectToSend = FamilyBoosts[familyBoostsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var monsterBoostsCount = reader.ReadUShort();
            MonsterBoosts = new MonsterBoosts[monsterBoostsCount];
            for (var monsterBoostsIndex = 0; monsterBoostsIndex < monsterBoostsCount; monsterBoostsIndex++)
            {
                var objectToAdd = new MonsterBoosts();
                objectToAdd.Deserialize(reader);
                MonsterBoosts[monsterBoostsIndex] = objectToAdd;
            }
            var familyBoostsCount = reader.ReadUShort();
            FamilyBoosts = new MonsterBoosts[familyBoostsCount];
            for (var familyBoostsIndex = 0; familyBoostsIndex < familyBoostsCount; familyBoostsIndex++)
            {
                var objectToAdd = new MonsterBoosts();
                objectToAdd.Deserialize(reader);
                FamilyBoosts[familyBoostsIndex] = objectToAdd;
            }
        }

    }
}
