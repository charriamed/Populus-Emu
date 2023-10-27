namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HaapiApiKeyMessage : Message
    {
        public const uint Id = 6649;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Token { get; set; }

        public HaapiApiKeyMessage(string token)
        {
            this.Token = token;
        }

        public HaapiApiKeyMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Token);
        }

        public override void Deserialize(IDataReader reader)
        {
            Token = reader.ReadUTF();
        }

    }
}
