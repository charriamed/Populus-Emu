namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BasicLatencyStatsMessage : Message
    {
        public const uint Id = 5663;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort Latency { get; set; }
        public ushort SampleCount { get; set; }
        public ushort Max { get; set; }

        public BasicLatencyStatsMessage(ushort latency, ushort sampleCount, ushort max)
        {
            this.Latency = latency;
            this.SampleCount = sampleCount;
            this.Max = max;
        }

        public BasicLatencyStatsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUShort(Latency);
            writer.WriteVarUShort(SampleCount);
            writer.WriteVarUShort(Max);
        }

        public override void Deserialize(IDataReader reader)
        {
            Latency = reader.ReadUShort();
            SampleCount = reader.ReadVarUShort();
            Max = reader.ReadVarUShort();
        }

    }
}
