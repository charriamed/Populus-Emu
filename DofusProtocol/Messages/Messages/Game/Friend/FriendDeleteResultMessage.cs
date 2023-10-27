namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FriendDeleteResultMessage : Message
    {
        public const uint Id = 5601;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Success { get; set; }
        public string Name { get; set; }

        public FriendDeleteResultMessage(bool success, string name)
        {
            this.Success = success;
            this.Name = name;
        }

        public FriendDeleteResultMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Success);
            writer.WriteUTF(Name);
        }

        public override void Deserialize(IDataReader reader)
        {
            Success = reader.ReadBoolean();
            Name = reader.ReadUTF();
        }

    }
}
