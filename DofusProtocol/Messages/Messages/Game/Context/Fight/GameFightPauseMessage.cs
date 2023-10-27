namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightPauseMessage : Message
    {
        public const uint Id = 6754;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool IsPaused { get; set; }

        public GameFightPauseMessage(bool isPaused)
        {
            this.IsPaused = isPaused;
        }

        public GameFightPauseMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(IsPaused);
        }

        public override void Deserialize(IDataReader reader)
        {
            IsPaused = reader.ReadBoolean();
        }

    }
}
