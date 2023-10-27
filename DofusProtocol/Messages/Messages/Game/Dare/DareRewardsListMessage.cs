namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class DareRewardsListMessage : Message
    {
        public const uint Id = 6677;
        public override uint MessageId
        {
            get { return Id; }
        }
        public DareReward[] Rewards { get; set; }

        public DareRewardsListMessage(DareReward[] rewards)
        {
            this.Rewards = rewards;
        }

        public DareRewardsListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Rewards.Count());
            for (var rewardsIndex = 0; rewardsIndex < Rewards.Count(); rewardsIndex++)
            {
                var objectToSend = Rewards[rewardsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var rewardsCount = reader.ReadUShort();
            Rewards = new DareReward[rewardsCount];
            for (var rewardsIndex = 0; rewardsIndex < rewardsCount; rewardsIndex++)
            {
                var objectToAdd = new DareReward();
                objectToAdd.Deserialize(reader);
                Rewards[rewardsIndex] = objectToAdd;
            }
        }

    }
}
