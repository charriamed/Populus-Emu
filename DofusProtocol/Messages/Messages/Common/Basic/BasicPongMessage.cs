namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BasicPongMessage : Message
    {
        public const uint Id = 183;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Quiet { get; set; }

        public BasicPongMessage(bool quiet)
        {
            this.Quiet = quiet;
        }

        public BasicPongMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Quiet);
        }

        public override void Deserialize(IDataReader reader)
        {
            Quiet = reader.ReadBoolean();
        }

    }
}
