namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeBidHouseGenericItemRemovedMessage : Message
    {
        public const uint Id = 5948;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort ObjGenericId { get; set; }

        public ExchangeBidHouseGenericItemRemovedMessage(ushort objGenericId)
        {
            this.ObjGenericId = objGenericId;
        }

        public ExchangeBidHouseGenericItemRemovedMessage() { }

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
