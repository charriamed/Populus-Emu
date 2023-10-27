namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class EmotePlayRequestMessage : Message
    {
        public const uint Id = 5685;
        public override uint MessageId
        {
            get { return Id; }
        }
        public byte EmoteId { get; set; }

        public EmotePlayRequestMessage(byte emoteId)
        {
            this.EmoteId = emoteId;
        }

        public EmotePlayRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(EmoteId);
        }

        public override void Deserialize(IDataReader reader)
        {
            EmoteId = reader.ReadByte();
        }

    }
}
