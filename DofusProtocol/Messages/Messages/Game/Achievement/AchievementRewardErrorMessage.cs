namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AchievementRewardErrorMessage : Message
    {
        public const uint Id = 6375;
        public override uint MessageId
        {
            get { return Id; }
        }
        public short AchievementId { get; set; }

        public AchievementRewardErrorMessage(short achievementId)
        {
            this.AchievementId = achievementId;
        }

        public AchievementRewardErrorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(AchievementId);
        }

        public override void Deserialize(IDataReader reader)
        {
            AchievementId = reader.ReadShort();
        }

    }
}
