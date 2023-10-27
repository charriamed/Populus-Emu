namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FriendJoinRequestMessage : Message
    {
        public const uint Id = 5605;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Name { get; set; }

        public FriendJoinRequestMessage(string name)
        {
            this.Name = name;
        }

        public FriendJoinRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Name);
        }

        public override void Deserialize(IDataReader reader)
        {
            Name = reader.ReadUTF();
        }

    }
}
