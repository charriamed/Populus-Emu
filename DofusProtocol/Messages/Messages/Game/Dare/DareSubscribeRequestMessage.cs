namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class DareSubscribeRequestMessage : Message
    {
        public const uint Id = 6666;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double DareId { get; set; }
        public bool Subscribe { get; set; }

        public DareSubscribeRequestMessage(double dareId, bool subscribe)
        {
            this.DareId = dareId;
            this.Subscribe = subscribe;
        }

        public DareSubscribeRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(DareId);
            writer.WriteBoolean(Subscribe);
        }

        public override void Deserialize(IDataReader reader)
        {
            DareId = reader.ReadDouble();
            Subscribe = reader.ReadBoolean();
        }

    }
}
