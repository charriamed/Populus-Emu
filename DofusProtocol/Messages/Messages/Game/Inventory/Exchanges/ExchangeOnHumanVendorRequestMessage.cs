namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeOnHumanVendorRequestMessage : Message
    {
        public const uint Id = 5772;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong HumanVendorId { get; set; }
        public ushort HumanVendorCell { get; set; }

        public ExchangeOnHumanVendorRequestMessage(ulong humanVendorId, ushort humanVendorCell)
        {
            this.HumanVendorId = humanVendorId;
            this.HumanVendorCell = humanVendorCell;
        }

        public ExchangeOnHumanVendorRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(HumanVendorId);
            writer.WriteVarUShort(HumanVendorCell);
        }

        public override void Deserialize(IDataReader reader)
        {
            HumanVendorId = reader.ReadVarULong();
            HumanVendorCell = reader.ReadVarUShort();
        }

    }
}
