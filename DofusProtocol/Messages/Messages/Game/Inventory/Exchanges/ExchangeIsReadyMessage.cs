namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeIsReadyMessage : Message
    {
        public const uint Id = 5509;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double ObjectId { get; set; }
        public bool Ready { get; set; }

        public ExchangeIsReadyMessage(double objectId, bool ready)
        {
            this.ObjectId = objectId;
            this.Ready = ready;
        }

        public ExchangeIsReadyMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(ObjectId);
            writer.WriteBoolean(Ready);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadDouble();
            Ready = reader.ReadBoolean();
        }

    }
}
