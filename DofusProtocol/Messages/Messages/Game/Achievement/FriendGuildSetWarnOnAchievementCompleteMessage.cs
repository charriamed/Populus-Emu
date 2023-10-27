namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FriendGuildSetWarnOnAchievementCompleteMessage : Message
    {
        public const uint Id = 6382;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Enable { get; set; }

        public FriendGuildSetWarnOnAchievementCompleteMessage(bool enable)
        {
            this.Enable = enable;
        }

        public FriendGuildSetWarnOnAchievementCompleteMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Enable);
        }

        public override void Deserialize(IDataReader reader)
        {
            Enable = reader.ReadBoolean();
        }

    }
}
