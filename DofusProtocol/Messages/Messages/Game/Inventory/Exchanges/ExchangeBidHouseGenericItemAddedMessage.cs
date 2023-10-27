namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeBidHouseGenericItemAddedMessage : Message
    {
        public const uint Id = 5947;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort ObjGenericId { get; set; }

        public ExchangeBidHouseGenericItemAddedMessage(ushort objGenericId)
        {
            this.ObjGenericId = objGenericId;
        }

        public ExchangeBidHouseGenericItemAddedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ObjGenericId);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjGenericId = reader.ReadVarUShort();
        }

    }
}
