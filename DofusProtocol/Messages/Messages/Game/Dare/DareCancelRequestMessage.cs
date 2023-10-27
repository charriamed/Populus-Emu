namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class DareCancelRequestMessage : Message
    {
        public const uint Id = 6680;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double DareId { get; set; }

        public DareCancelRequestMessage(double dareId)
        {
            this.DareId = dareId;
        }

        public DareCancelRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(DareId);
        }

        public override void Deserialize(IDataReader reader)
        {
            DareId = reader.ReadDouble();
        }

    }
}
