namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CheckFileRequestMessage : Message
    {
        public const uint Id = 6154;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Filename { get; set; }
        public sbyte Type { get; set; }

        public CheckFileRequestMessage(string filename, sbyte type)
        {
            this.Filename = filename;
            this.Type = type;
        }

        public CheckFileRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Filename);
            writer.WriteSByte(Type);
        }

        public override void Deserialize(IDataReader reader)
        {
            Filename = reader.ReadUTF();
            Type = reader.ReadSByte();
        }

    }
}
