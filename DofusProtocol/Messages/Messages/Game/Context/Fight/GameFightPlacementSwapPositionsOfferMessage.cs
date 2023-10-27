namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightPlacementSwapPositionsOfferMessage : Message
    {
        public const uint Id = 6542;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int RequestId { get; set; }
        public double RequesterId { get; set; }
        public ushort RequesterCellId { get; set; }
        public double RequestedId { get; set; }
        public ushort RequestedCellId { get; set; }

        public GameFightPlacementSwapPositionsOfferMessage(int requestId, double requesterId, ushort requesterCellId, double requestedId, ushort requestedCellId)
        {
            this.RequestId = requestId;
            this.RequesterId = requesterId;
            this.RequesterCellId = requesterCellId;
            this.RequestedId = requestedId;
            this.RequestedCellId = requestedCellId;
        }

        public GameFightPlacementSwapPositionsOfferMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(RequestId);
            writer.WriteDouble(RequesterId);
            writer.WriteVarUShort(RequesterCellId);
            writer.WriteDouble(RequestedId);
            writer.WriteVarUShort(RequestedCellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            RequestId = reader.ReadInt();
            RequesterId = reader.ReadDouble();
            RequesterCellId = reader.ReadVarUShort();
            RequestedId = reader.ReadDouble();
            RequestedCellId = reader.ReadVarUShort();
        }

    }
}
