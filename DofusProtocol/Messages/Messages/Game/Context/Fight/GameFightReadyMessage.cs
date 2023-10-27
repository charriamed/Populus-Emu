namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightReadyMessage : Message
    {
        public const uint Id = 708;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool IsReady { get; set; }

        public GameFightReadyMessage(bool isReady)
        {
            this.IsReady = isReady;
        }

        public GameFightReadyMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(IsReady);
        }

        public override void Deserialize(IDataReader reader)
        {
            IsReady = reader.ReadBoolean();
        }

    }
}
