namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class BreachRewardsMessage : Message
    {
        public const uint Id = 6813;
        public override uint MessageId
        {
            get { return Id; }
        }
        public BreachReward[] Rewards { get; set; }
        public BreachReward Save { get; set; }

        public BreachRewardsMessage(BreachReward[] rewards, BreachReward save)
        {
            this.Rewards = rewards;
            this.Save = save;
        }

        public BreachRewardsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Rewards.Count());
            for (var rewardsIndex = 0; rewardsIndex < Rewards.Count(); rewardsIndex++)
            {
                var objectToSend = Rewards[rewardsIndex];
                objectToSend.Serialize(writer);
            }
            Save.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            var rewardsCount = reader.ReadUShort();
            Rewards = new BreachReward[rewardsCount];
            for (var rewardsIndex = 0; rewardsIndex < rewardsCount; rewardsIndex++)
            {
                var objectToAdd = new BreachReward();
                objectToAdd.Deserialize(reader);
                Rewards[rewardsIndex] = objectToAdd;
            }
            Save = new BreachReward();
            Save.Deserialize(reader);
        }

    }
}
