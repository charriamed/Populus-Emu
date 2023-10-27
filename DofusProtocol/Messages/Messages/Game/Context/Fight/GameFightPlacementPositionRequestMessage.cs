namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightPlacementPositionRequestMessage : Message
    {
        public const uint Id = 704;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort CellId { get; set; }

        public GameFightPlacementPositionRequestMessage(ushort cellId)
        {
            this.CellId = cellId;
        }

        public GameFightPlacementPositionRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(CellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            CellId = reader.ReadVarUShort();
        }

    }
}
