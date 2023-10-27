namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExtendedBreachBranch : BreachBranch
    {
        public new const short Id = 560;
        public override short TypeId
        {
            get { return Id; }
        }
        public MonsterInGroupLightInformations[] Monsters { get; set; }
        public BreachReward[] Rewards { get; set; }
        public uint Modifier { get; set; }
        public uint Prize { get; set; }

        public ExtendedBreachBranch(sbyte room, int element, MonsterInGroupLightInformations[] bosses, double map, MonsterInGroupLightInformations[] monsters, BreachReward[] rewards, uint modifier, uint prize)
        {
            this.Room = room;
            this.Element = element;
            this.Bosses = bosses;
            this.Map = map;
            this.Monsters = monsters;
            this.Rewards = rewards;
            this.Modifier = modifier;
            this.Prize = prize;
        }

        public ExtendedBreachBranch() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)Monsters.Count());
            for (var monstersIndex = 0; monstersIndex < Monsters.Count(); monstersIndex++)
            {
                var objectToSend = Monsters[monstersIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)Rewards.Count());
            for (var rewardsIndex = 0; rewardsIndex < Rewards.Count(); rewardsIndex++)
            {
                var objectToSend = Rewards[rewardsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteVarUInt(Modifier);
            writer.WriteVarUInt(Prize);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var monstersCount = reader.ReadUShort();
            Monsters = new MonsterInGroupLightInformations[monstersCount];
            for (var monstersIndex = 0; monstersIndex < monstersCount; monstersIndex++)
            {
                var objectToAdd = new MonsterInGroupLightInformations();
                objectToAdd.Deserialize(reader);
                Monsters[monstersIndex] = objectToAdd;
            }
            var rewardsCount = reader.ReadUShort();
            Rewards = new BreachReward[rewardsCount];
            for (var rewardsIndex = 0; rewardsIndex < rewardsCount; rewardsIndex++)
            {
                var objectToAdd = new BreachReward();
                objectToAdd.Deserialize(reader);
                Rewards[rewardsIndex] = objectToAdd;
            }
            Modifier = reader.ReadVarUInt();
            Prize = reader.ReadVarUInt();
        }

    }
}
