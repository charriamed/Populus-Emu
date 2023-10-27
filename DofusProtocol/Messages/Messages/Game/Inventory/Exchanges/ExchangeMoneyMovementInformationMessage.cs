namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeMoneyMovementInformationMessage : Message
    {
        public const uint Id = 6834;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong Limit { get; set; }

        public ExchangeMoneyMovementInformationMessage(ulong limit)
        {
            this.Limit = limit;
        }

        public ExchangeMoneyMovementInformationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(Limit);
        }

        public override void Deserialize(IDataReader reader)
        {
            Limit = reader.ReadVarULong();
        }

    }
}
