namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightPlacementSwapPositionsMessage : Message
    {
        public const uint Id = 6544;
        public override uint MessageId
        {
            get { return Id; }
        }

        public GameFightPlacementSwapPositionsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
        }

        public override void Deserialize(IDataReader reader)
        {
        }

    }
}
