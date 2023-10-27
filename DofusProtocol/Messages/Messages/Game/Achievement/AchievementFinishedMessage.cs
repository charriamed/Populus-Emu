namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AchievementFinishedMessage : Message
    {
        public const uint Id = 6208;
        public override uint MessageId
        {
            get { return Id; }
        }
        public AchievementAchievedRewardable Achievement { get; set; }

        public AchievementFinishedMessage(AchievementAchievedRewardable achievement)
        {
            this.Achievement = achievement;
        }

        public AchievementFinishedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Achievement.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Achievement = new AchievementAchievedRewardable();
            Achievement.Deserialize(reader);
        }

    }
}
