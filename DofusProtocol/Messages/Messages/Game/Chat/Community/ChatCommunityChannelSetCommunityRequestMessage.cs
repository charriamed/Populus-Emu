namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ChatCommunityChannelSetCommunityRequestMessage : Message
    {
        public const uint Id = 6729;
        public override uint MessageId
        {
            get { return Id; }
        }
        public short CommunityId { get; set; }

        public ChatCommunityChannelSetCommunityRequestMessage(short communityId)
        {
            this.CommunityId = communityId;
        }

        public ChatCommunityChannelSetCommunityRequestMessage() { }

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
