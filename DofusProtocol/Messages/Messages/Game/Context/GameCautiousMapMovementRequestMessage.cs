namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameCautiousMapMovementRequestMessage : GameMapMovementRequestMessage
    {
        public new const uint Id = 6496;
        public override uint MessageId
        {
            get { return Id; }
        }

        public GameCautiousMapMovementRequestMessage(short[] keyMovements, double mapId)
        {
            this.KeyMovements = keyMovements;
            this.MapId = mapId;
        }

        public GameCautiousMapMovementRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }

    }
}
