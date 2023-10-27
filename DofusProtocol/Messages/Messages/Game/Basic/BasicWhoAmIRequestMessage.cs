namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BasicWhoAmIRequestMessage : Message
    {
        public const uint Id = 5664;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Verbose { get; set; }

        public BasicWhoAmIRequestMessage(bool verbose)
        {
            this.Verbose = verbose;
        }

        public BasicWhoAmIRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Verbose);
        }

        public override void Deserialize(IDataReader reader)
        {
            Verbose = reader.ReadBoolean();
        }

    }
}
