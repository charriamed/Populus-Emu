namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeStartOkNpcTradeMessage : Message
    {
        public const uint Id = 5785;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double NpcId { get; set; }

        public ExchangeStartOkNpcTradeMessage(double npcId)
        {
            this.NpcId = npcId;
        }

        public ExchangeStartOkNpcTradeMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(NpcId);
        }

        public override void Deserialize(IDataReader reader)
        {
            NpcId = reader.ReadDouble();
        }

    }
}
