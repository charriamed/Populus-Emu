namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FriendSpouseFollowWithCompassRequestMessage : Message
    {
        public const uint Id = 5606;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Enable { get; set; }

        public FriendSpouseFollowWithCompassRequestMessage(bool enable)
        {
            this.Enable = enable;
        }

        public FriendSpouseFollowWithCompassRequestMessage() { }

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
