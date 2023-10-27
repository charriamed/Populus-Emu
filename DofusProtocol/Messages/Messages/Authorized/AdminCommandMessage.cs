namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AdminCommandMessage : Message
    {
        public const uint Id = 76;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Content { get; set; }

        public AdminCommandMessage(string content)
        {
            this.Content = content;
        }

        public AdminCommandMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Content);
        }

        public override void Deserialize(IDataReader reader)
        {
            Content = reader.ReadUTF();
        }

    }
}
