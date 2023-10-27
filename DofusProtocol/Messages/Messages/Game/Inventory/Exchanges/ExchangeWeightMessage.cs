namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeWeightMessage : Message
    {
        public const uint Id = 5793;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint CurrentWeight { get; set; }
        public uint MaxWeight { get; set; }

        public ExchangeWeightMessage(uint currentWeight, uint maxWeight)
        {
            this.CurrentWeight = currentWeight;
            this.MaxWeight = maxWeight;
        }

        public ExchangeWeightMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(CurrentWeight);
            writer.WriteVarUInt(MaxWeight);
        }

        public override void Deserialize(IDataReader reader)
        {
            CurrentWeight = reader.ReadVarUInt();
            MaxWeight = reader.ReadVarUInt();
        }

    }
}
