namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AchievementDetailsMessage : Message
    {
        public const uint Id = 6378;
        public override uint MessageId
        {
            get { return Id; }
        }
        public Achievement Achievement { get; set; }

        public AchievementDetailsMessage(Achievement achievement)
        {
            this.Achievement = achievement;
        }

        public AchievementDetailsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Achievement.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Achievement = new Achievement();
            Achievement.Deserialize(reader);
        }

    }
}
