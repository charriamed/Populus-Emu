namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeBidHouseBuyResultMessage : Message
    {
        public const uint Id = 6272;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint Uid { get; set; }
        public bool Bought { get; set; }

        public ExchangeBidHouseBuyResultMessage(uint uid, bool bought)
        {
            this.Uid = uid;
            this.Bought = bought;
        }

        public ExchangeBidHouseBuyResultMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(Uid);
            writer.WriteBoolean(Bought);
        }

        public override void Deserialize(IDataReader reader)
        {
            Uid = reader.ReadVarUInt();
            Bought = reader.ReadBoolean();
        }

    }
}
