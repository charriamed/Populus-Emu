namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HaapiTokenMessage : Message
    {
        public const uint Id = 6767;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Token { get; set; }

        public HaapiTokenMessage(string token)
        {
            this.Token = token;
        }

        public HaapiTokenMessage() { }

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
