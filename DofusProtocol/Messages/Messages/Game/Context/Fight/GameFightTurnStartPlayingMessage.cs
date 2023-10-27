namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightTurnStartPlayingMessage : Message
    {
        public const uint Id = 6465;
        public override uint MessageId
        {
            get { return Id; }
        }

        public GameFightTurnStartPlayingMessage() { }

        public override void Serialize(IDataWriter writer)
        {
        }

        public override void Deserialize(IDataReader reader)
        {
        }

    }
}
