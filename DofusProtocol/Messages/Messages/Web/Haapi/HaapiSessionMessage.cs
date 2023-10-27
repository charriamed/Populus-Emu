namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HaapiSessionMessage : Message
    {
        public const uint Id = 6769;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Key { get; set; }
        public sbyte Type { get; set; }

        public HaapiSessionMessage(string key, sbyte type)
        {
            this.Key = key;
            this.Type = type;
        }

        public HaapiSessionMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Key);
            writer.WriteSByte(Type);
        }

        public override void Deserialize(IDataReader reader)
        {
            Key = reader.ReadUTF();
            Type = reader.ReadSByte();
        }

    }
}
