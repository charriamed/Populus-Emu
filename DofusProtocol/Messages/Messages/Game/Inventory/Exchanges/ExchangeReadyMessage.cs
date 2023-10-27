namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeReadyMessage : Message
    {
        public const uint Id = 5511;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Ready { get; set; }
        public ushort Step { get; set; }

        public ExchangeReadyMessage(bool ready, ushort step)
        {
            this.Ready = ready;
            this.Step = step;
        }

        public ExchangeReadyMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Ready);
            writer.WriteVarUShort(Step);
        }

        public override void Deserialize(IDataReader reader)
        {
            Ready = reader.ReadBoolean();
            Step = reader.ReadVarUShort();
        }

    }
}
