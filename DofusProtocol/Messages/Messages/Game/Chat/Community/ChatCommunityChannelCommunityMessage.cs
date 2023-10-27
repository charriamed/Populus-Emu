namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ChatCommunityChannelCommunityMessage : Message
    {
        public const uint Id = 6730;
        public override uint MessageId
        {
            get { return Id; }
        }
        public short CommunityId { get; set; }

        public ChatCommunityChannelCommunityMessage(short communityId)
        {
            this.CommunityId = communityId;
        }

        public ChatCommunityChannelCommunityMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(CommunityId);
        }

        public override void Deserialize(IDataReader reader)
        {
            CommunityId = reader.ReadShort();
        }

    }
}
