namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeBidHouseSearchMessage : Message
    {
        public const uint Id = 5806;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint Type { get; set; }
        public ushort GenId { get; set; }

        public ExchangeBidHouseSearchMessage(uint type, ushort genId)
        {
            this.Type = type;
            this.GenId = genId;
        }

        public ExchangeBidHouseSearchMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(Type);
            writer.WriteVarUShort(GenId);
        }

        public override void Deserialize(IDataReader reader)
        {
            Type = reader.ReadVarUInt();
            GenId = reader.ReadVarUShort();
        }

    }
}
