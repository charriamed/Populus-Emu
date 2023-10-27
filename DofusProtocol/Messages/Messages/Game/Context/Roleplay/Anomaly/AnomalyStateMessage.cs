namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AnomalyStateMessage : Message
    {
        public const uint Id = 6831;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Open { get; set; }
        public ulong ClosingTime { get; set; }

        public AnomalyStateMessage(bool open, ulong closingTime)
        {
            this.Open = open;
            this.ClosingTime = closingTime;
        }

        public AnomalyStateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Open);
            writer.WriteVarULong(ClosingTime);
        }

        public override void Deserialize(IDataReader reader)
        {
            Open = reader.ReadBoolean();
            ClosingTime = reader.ReadVarULong();
        }

    }
}
