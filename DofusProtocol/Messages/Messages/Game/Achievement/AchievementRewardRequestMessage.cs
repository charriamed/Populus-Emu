namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AchievementRewardRequestMessage : Message
    {
        public const uint Id = 6377;
        public override uint MessageId
        {
            get { return Id; }
        }
        public short AchievementId { get; set; }

        public AchievementRewardRequestMessage(short achievementId)
        {
            this.AchievementId = achievementId;
        }

        public AchievementRewardRequestMessage() { }

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
