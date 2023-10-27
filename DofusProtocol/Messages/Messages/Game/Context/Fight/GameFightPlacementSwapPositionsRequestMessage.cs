namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightPlacementSwapPositionsRequestMessage : GameFightPlacementPositionRequestMessage
    {
        public new const uint Id = 6541;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double RequestedId { get; set; }

        public GameFightPlacementSwapPositionsRequestMessage(ushort cellId, double requestedId)
        {
            this.CellId = cellId;
            this.RequestedId = requestedId;
        }

        public GameFightPlacementSwapPositionsRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(RequestedId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            RequestedId = reader.ReadDouble();
        }

    }
}
