namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TaxCollectorMovementRemoveMessage : Message
    {
        public const uint Id = 5915;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double CollectorId { get; set; }

        public TaxCollectorMovementRemoveMessage(double collectorId)
        {
            this.CollectorId = collectorId;
        }

        public TaxCollectorMovementRemoveMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(CollectorId);
        }

        public override void Deserialize(IDataReader reader)
        {
            CollectorId = reader.ReadDouble();
        }

    }
}
