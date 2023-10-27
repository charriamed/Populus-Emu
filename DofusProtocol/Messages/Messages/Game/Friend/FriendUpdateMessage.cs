namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FriendUpdateMessage : Message
    {
        public const uint Id = 5924;
        public override uint MessageId
        {
            get { return Id; }
        }
        public FriendInformations FriendUpdated { get; set; }

        public FriendUpdateMessage(FriendInformations friendUpdated)
        {
            this.FriendUpdated = friendUpdated;
        }

        public FriendUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(FriendUpdated.TypeId);
            FriendUpdated.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            FriendUpdated = ProtocolTypeManager.GetInstance<FriendInformations>(reader.ReadShort());
            FriendUpdated.Deserialize(reader);
        }

    }
}
