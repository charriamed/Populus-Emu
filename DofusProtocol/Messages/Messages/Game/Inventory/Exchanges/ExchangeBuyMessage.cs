namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeBuyMessage : Message
    {
        public const uint Id = 5774;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint ObjectToBuyId { get; set; }
        public uint Quantity { get; set; }

        public ExchangeBuyMessage(uint objectToBuyId, uint quantity)
        {
            this.ObjectToBuyId = objectToBuyId;
            this.Quantity = quantity;
        }

        public ExchangeBuyMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ObjectToBuyId);
            writer.WriteVarUInt(Quantity);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectToBuyId = reader.ReadVarUInt();
            Quantity = reader.ReadVarUInt();
        }

    }
}
