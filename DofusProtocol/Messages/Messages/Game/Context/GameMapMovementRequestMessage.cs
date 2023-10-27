namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameMapMovementRequestMessage : Message
    {
        public const uint Id = 950;
        public override uint MessageId
        {
            get { return Id; }
        }
        public short[] KeyMovements { get; set; }
        public double MapId { get; set; }

        public GameMapMovementRequestMessage(short[] keyMovements, double mapId)
        {
            this.KeyMovements = keyMovements;
            this.MapId = mapId;
        }

        public GameMapMovementRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)KeyMovements.Count());
            for (var keyMovementsIndex = 0; keyMovementsIndex < KeyMovements.Count(); keyMovementsIndex++)
            {
                writer.WriteShort(KeyMovements[keyMovementsIndex]);
            }
            writer.WriteDouble(MapId);
        }

        public override void Deserialize(IDataReader reader)
        {
            var keyMovementsCount = reader.ReadUShort();
            KeyMovements = new short[keyMovementsCount];
            for (var keyMovementsIndex = 0; keyMovementsIndex < keyMovementsCount; keyMovementsIndex++)
            {
                KeyMovements[keyMovementsIndex] = reader.ReadShort();
            }
            MapId = reader.ReadDouble();
        }

    }
}
