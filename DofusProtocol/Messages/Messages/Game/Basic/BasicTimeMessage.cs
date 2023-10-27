namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BasicTimeMessage : Message
    {
        public const uint Id = 175;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double Timestamp { get; set; }
        public short TimezoneOffset { get; set; }

        public BasicTimeMessage(double timestamp, short timezoneOffset)
        {
            this.Timestamp = timestamp;
            this.TimezoneOffset = timezoneOffset;
        }

        public BasicTimeMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(Timestamp);
            writer.WriteShort(TimezoneOffset);
        }

        public override void Deserialize(IDataReader reader)
        {
            Timestamp = reader.ReadDouble();
            TimezoneOffset = reader.ReadShort();
        }

    }
}
