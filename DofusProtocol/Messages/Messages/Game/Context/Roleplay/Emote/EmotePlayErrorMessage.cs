namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class EmotePlayErrorMessage : Message
    {
        public const uint Id = 5688;
        public override uint MessageId
        {
            get { return Id; }
        }
        public byte EmoteId { get; set; }

        public EmotePlayErrorMessage(byte emoteId)
        {
            this.EmoteId = emoteId;
        }

        public EmotePlayErrorMessage() { }

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
