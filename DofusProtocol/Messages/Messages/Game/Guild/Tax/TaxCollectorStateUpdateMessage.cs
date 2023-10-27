namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TaxCollectorStateUpdateMessage : Message
    {
        public const uint Id = 6455;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double UniqueId { get; set; }
        public sbyte State { get; set; }

        public TaxCollectorStateUpdateMessage(double uniqueId, sbyte state)
        {
            this.UniqueId = uniqueId;
            this.State = state;
        }

        public TaxCollectorStateUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(UniqueId);
            writer.WriteSByte(State);
        }

        public override void Deserialize(IDataReader reader)
        {
            UniqueId = reader.ReadDouble();
            State = reader.ReadSByte();
        }

    }
}
