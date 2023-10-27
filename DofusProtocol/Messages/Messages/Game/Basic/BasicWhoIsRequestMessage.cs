namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BasicWhoIsRequestMessage : Message
    {
        public const uint Id = 181;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Verbose { get; set; }
        public string Search { get; set; }

        public BasicWhoIsRequestMessage(bool verbose, string search)
        {
            this.Verbose = verbose;
            this.Search = search;
        }

        public BasicWhoIsRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Verbose);
            writer.WriteUTF(Search);
        }

        public override void Deserialize(IDataReader reader)
        {
            Verbose = reader.ReadBoolean();
            Search = reader.ReadUTF();
        }

    }
}
