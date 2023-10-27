namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangePodsModifiedMessage : ExchangeObjectMessage
    {
        public new const uint Id = 6670;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint CurrentWeight { get; set; }
        public uint MaxWeight { get; set; }

        public ExchangePodsModifiedMessage(bool remote, uint currentWeight, uint maxWeight)
        {
            this.Remote = remote;
            this.CurrentWeight = currentWeight;
            this.MaxWeight = maxWeight;
        }

        public ExchangePodsModifiedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(CurrentWeight);
            writer.WriteVarUInt(MaxWeight);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            CurrentWeight = reader.ReadVarUInt();
            MaxWeight = reader.ReadVarUInt();
        }

    }
}
