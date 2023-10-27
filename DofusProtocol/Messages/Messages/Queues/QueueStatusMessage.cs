namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class QueueStatusMessage : Message
    {
        public const uint Id = 6100;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort Position { get; set; }
        public ushort Total { get; set; }

        public QueueStatusMessage(ushort position, ushort total)
        {
            this.Position = position;
            this.Total = total;
        }

        public QueueStatusMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUShort(Position);
            writer.WriteUShort(Total);
        }

        public override void Deserialize(IDataReader reader)
        {
            Position = reader.ReadUShort();
            Total = reader.ReadUShort();
        }

    }
}
