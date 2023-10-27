namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightPlacementSwapPositionsCancelledMessage : Message
    {
        public const uint Id = 6546;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int RequestId { get; set; }
        public double CancellerId { get; set; }

        public GameFightPlacementSwapPositionsCancelledMessage(int requestId, double cancellerId)
        {
            this.RequestId = requestId;
            this.CancellerId = cancellerId;
        }

        public GameFightPlacementSwapPositionsCancelledMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(RequestId);
            writer.WriteDouble(CancellerId);
        }

        public override void Deserialize(IDataReader reader)
        {
            RequestId = reader.ReadInt();
            CancellerId = reader.ReadDouble();
        }

    }
}
