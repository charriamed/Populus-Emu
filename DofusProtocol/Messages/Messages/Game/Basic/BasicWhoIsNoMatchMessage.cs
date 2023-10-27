namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BasicWhoIsNoMatchMessage : Message
    {
        public const uint Id = 179;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Search { get; set; }

        public BasicWhoIsNoMatchMessage(string search)
        {
            this.Search = search;
        }

        public BasicWhoIsNoMatchMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Search);
        }

        public override void Deserialize(IDataReader reader)
        {
            Search = reader.ReadUTF();
        }

    }
}
