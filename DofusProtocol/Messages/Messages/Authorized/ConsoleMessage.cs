namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ConsoleMessage : Message
    {
        public const uint Id = 75;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Type { get; set; }
        public string Content { get; set; }

        public ConsoleMessage(sbyte type, string content)
        {
            this.Type = type;
            this.Content = content;
        }

        public ConsoleMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Type);
            writer.WriteUTF(Content);
        }

        public override void Deserialize(IDataReader reader)
        {
            Type = reader.ReadSByte();
            Content = reader.ReadUTF();
        }

    }
}
