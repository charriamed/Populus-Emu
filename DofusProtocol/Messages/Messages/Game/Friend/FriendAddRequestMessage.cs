namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FriendAddRequestMessage : Message
    {
        public const uint Id = 4004;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Name { get; set; }

        public FriendAddRequestMessage(string name)
        {
            this.Name = name;
        }

        public FriendAddRequestMessage() { }

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
