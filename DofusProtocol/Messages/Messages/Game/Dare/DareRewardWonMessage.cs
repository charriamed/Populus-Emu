namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class DareRewardWonMessage : Message
    {
        public const uint Id = 6678;
        public override uint MessageId
        {
            get { return Id; }
        }
        public DareReward Reward { get; set; }

        public DareRewardWonMessage(DareReward reward)
        {
            this.Reward = reward;
        }

        public DareRewardWonMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Reward.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Reward = new DareReward();
            Reward.Deserialize(reader);
        }

    }
}
