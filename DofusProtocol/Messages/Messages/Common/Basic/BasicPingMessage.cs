namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BasicPingMessage : Message
    {
        public const uint Id = 182;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Quiet { get; set; }

        public BasicPingMessage(bool quiet)
        {
            this.Quiet = quiet;
        }

        public BasicPingMessage() { }

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
