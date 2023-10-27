namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeWaitingResultMessage : Message
    {
        public const uint Id = 5786;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Bwait { get; set; }

        public ExchangeWaitingResultMessage(bool bwait)
        {
            this.Bwait = bwait;
        }

        public ExchangeWaitingResultMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Bwait);
        }

        public override void Deserialize(IDataReader reader)
        {
            Bwait = reader.ReadBoolean();
        }

    }
}
