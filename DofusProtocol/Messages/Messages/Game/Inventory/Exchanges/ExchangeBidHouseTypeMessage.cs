namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeBidHouseTypeMessage : Message
    {
        public const uint Id = 5803;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint Type { get; set; }
        public bool Follow { get; set; }

        public ExchangeBidHouseTypeMessage(uint type, bool follow)
        {
            this.Type = type;
            this.Follow = follow;
        }

        public ExchangeBidHouseTypeMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(Type);
            writer.WriteBoolean(Follow);
        }

        public override void Deserialize(IDataReader reader)
        {
            Type = reader.ReadVarUInt();
            Follow = reader.ReadBoolean();
        }

    }
}
