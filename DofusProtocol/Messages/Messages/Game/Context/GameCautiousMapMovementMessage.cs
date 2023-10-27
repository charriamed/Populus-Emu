namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameCautiousMapMovementMessage : GameMapMovementMessage
    {
        public new const uint Id = 6497;
        public override uint MessageId
        {
            get { return Id; }
        }

        public GameCautiousMapMovementMessage(short[] keyMovements, short forcedDirection, double actorId)
        {
            this.KeyMovements = keyMovements;
            this.ForcedDirection = forcedDirection;
            this.ActorId = actorId;
        }

        public GameCautiousMapMovementMessage() { }

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
