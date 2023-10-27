namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AchievementDetailsRequestMessage : Message
    {
        public const uint Id = 6380;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort AchievementId { get; set; }

        public AchievementDetailsRequestMessage(ushort achievementId)
        {
            this.AchievementId = achievementId;
        }

        public AchievementDetailsRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(AchievementId);
        }

        public override void Deserialize(IDataReader reader)
        {
            AchievementId = reader.ReadVarUShort();
        }

    }
}
