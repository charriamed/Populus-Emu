namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class LoginQueueStatusMessage : Message
    {
        public const uint Id = 10;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort Position { get; set; }
        public ushort Total { get; set; }

        public LoginQueueStatusMessage(ushort position, ushort total)
        {
            this.Position = position;
            this.Total = total;
        }

        public LoginQueueStatusMessage() { }

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
