namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameMapNoMovementMessage : Message
    {
        public const uint Id = 954;
        public override uint MessageId
        {
            get { return Id; }
        }
        public short CellX { get; set; }
        public short CellY { get; set; }

        public GameMapNoMovementMessage(short cellX, short cellY)
        {
            this.CellX = cellX;
            this.CellY = cellY;
        }

        public GameMapNoMovementMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(CellX);
            writer.WriteShort(CellY);
        }

        public override void Deserialize(IDataReader reader)
        {
            CellX = reader.ReadShort();
            CellY = reader.ReadShort();
        }

    }
}
