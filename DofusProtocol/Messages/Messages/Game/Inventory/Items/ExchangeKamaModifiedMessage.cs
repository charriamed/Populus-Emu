namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeKamaModifiedMessage : ExchangeObjectMessage
    {
        public new const uint Id = 5521;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong Quantity { get; set; }

        public ExchangeKamaModifiedMessage(bool remote, ulong quantity)
        {
            this.Remote = remote;
            this.Quantity = quantity;
        }

        public ExchangeKamaModifiedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(Quantity);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Quantity = reader.ReadVarULong();
        }

    }
}
