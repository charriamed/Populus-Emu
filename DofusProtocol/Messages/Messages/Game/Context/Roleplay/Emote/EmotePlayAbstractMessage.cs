namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class EmotePlayAbstractMessage : Message
    {
        public const uint Id = 5690;
        public override uint MessageId
        {
            get { return Id; }
        }
        public byte EmoteId { get; set; }
        public double EmoteStartTime { get; set; }

        public EmotePlayAbstractMessage(byte emoteId, double emoteStartTime)
        {
            this.EmoteId = emoteId;
            this.EmoteStartTime = emoteStartTime;
        }

        public EmotePlayAbstractMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(EmoteId);
            writer.WriteDouble(EmoteStartTime);
        }

        public override void Deserialize(IDataReader reader)
        {
            EmoteId = reader.ReadByte();
            EmoteStartTime = reader.ReadDouble();
        }

    }
}
