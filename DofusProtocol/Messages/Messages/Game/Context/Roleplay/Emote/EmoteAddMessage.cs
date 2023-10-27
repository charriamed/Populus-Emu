namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class EmoteAddMessage : Message
    {
        public const uint Id = 5644;
        public override uint MessageId
        {
            get { return Id; }
        }
        public byte EmoteId { get; set; }

        public EmoteAddMessage(byte emoteId)
        {
            this.EmoteId = emoteId;
        }

        public EmoteAddMessage() { }

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
