namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ClientKeyMessage : Message
    {
        public const uint Id = 5607;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Key { get; set; }

        public ClientKeyMessage(string key)
        {
            this.Key = key;
        }

        public ClientKeyMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Key);
        }

        public override void Deserialize(IDataReader reader)
        {
            Key = reader.ReadUTF();
        }

    }
}
