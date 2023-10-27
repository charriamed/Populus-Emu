namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FriendSetStatusShareMessage : Message
    {
        public const uint Id = 6822;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Share { get; set; }

        public FriendSetStatusShareMessage(bool share)
        {
            this.Share = share;
        }

        public FriendSetStatusShareMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Share);
        }

        public override void Deserialize(IDataReader reader)
        {
            Share = reader.ReadBoolean();
        }

    }
}
