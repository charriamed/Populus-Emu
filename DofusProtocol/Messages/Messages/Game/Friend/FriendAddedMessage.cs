namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FriendAddedMessage : Message
    {
        public const uint Id = 5599;
        public override uint MessageId
        {
            get { return Id; }
        }
        public FriendInformations FriendAdded { get; set; }

        public FriendAddedMessage(FriendInformations friendAdded)
        {
            this.FriendAdded = friendAdded;
        }

        public FriendAddedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(FriendAdded.TypeId);
            FriendAdded.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            FriendAdded = ProtocolTypeManager.GetInstance<FriendInformations>(reader.ReadShort());
            FriendAdded.Deserialize(reader);
        }

    }
}
