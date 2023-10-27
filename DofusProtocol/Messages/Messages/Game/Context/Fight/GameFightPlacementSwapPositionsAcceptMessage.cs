namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightPlacementSwapPositionsAcceptMessage : Message
    {
        public const uint Id = 6547;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int RequestId { get; set; }

        public GameFightPlacementSwapPositionsAcceptMessage(int requestId)
        {
            this.RequestId = requestId;
        }

        public GameFightPlacementSwapPositionsAcceptMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(RequestId);
        }

        public override void Deserialize(IDataReader reader)
        {
            RequestId = reader.ReadInt();
        }

    }
}
