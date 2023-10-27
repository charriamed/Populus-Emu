namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class IgnoredAddRequestMessage : Message
    {
        public const uint Id = 5673;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Name { get; set; }
        public bool Session { get; set; }

        public IgnoredAddRequestMessage(string name, bool session)
        {
            this.Name = name;
            this.Session = session;
        }

        public IgnoredAddRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Name);
            writer.WriteBoolean(Session);
        }

        public override void Deserialize(IDataReader reader)
        {
            Name = reader.ReadUTF();
            Session = reader.ReadBoolean();
        }

    }
}
