namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CheckFileMessage : Message
    {
        public const uint Id = 6156;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string FilenameHash { get; set; }
        public sbyte Type { get; set; }
        public string Value { get; set; }

        public CheckFileMessage(string filenameHash, sbyte type, string value)
        {
            this.FilenameHash = filenameHash;
            this.Type = type;
            this.Value = value;
        }

        public CheckFileMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(FilenameHash);
            writer.WriteSByte(Type);
            writer.WriteUTF(Value);
        }

        public override void Deserialize(IDataReader reader)
        {
            FilenameHash = reader.ReadUTF();
            Type = reader.ReadSByte();
            Value = reader.ReadUTF();
        }

    }
}
