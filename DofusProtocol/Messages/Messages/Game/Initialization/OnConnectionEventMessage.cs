namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class OnConnectionEventMessage : Message
    {
        public const uint Id = 5726;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte EventType { get; set; }

        public OnConnectionEventMessage(sbyte eventType)
        {
            this.EventType = eventType;
        }

        public OnConnectionEventMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(EventType);
        }

        public override void Deserialize(IDataReader reader)
        {
            EventType = reader.ReadSByte();
        }

    }
}
