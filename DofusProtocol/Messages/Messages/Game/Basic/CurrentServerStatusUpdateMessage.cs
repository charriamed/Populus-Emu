namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CurrentServerStatusUpdateMessage : Message
    {
        public const uint Id = 6525;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Status { get; set; }

        public CurrentServerStatusUpdateMessage(sbyte status)
        {
            this.Status = status;
        }

        public CurrentServerStatusUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Status);
        }

        public override void Deserialize(IDataReader reader)
        {
            Status = reader.ReadSByte();
        }

    }
}
