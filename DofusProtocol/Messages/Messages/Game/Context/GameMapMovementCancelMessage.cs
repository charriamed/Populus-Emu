namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameMapMovementCancelMessage : Message
    {
        public const uint Id = 953;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort CellId { get; set; }

        public GameMapMovementCancelMessage(ushort cellId)
        {
            this.CellId = cellId;
        }

        public GameMapMovementCancelMessage() { }

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
