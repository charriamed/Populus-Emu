namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class IgnoredAddedMessage : Message
    {
        public const uint Id = 5678;
        public override uint MessageId
        {
            get { return Id; }
        }
        public IgnoredInformations IgnoreAdded { get; set; }
        public bool Session { get; set; }

        public IgnoredAddedMessage(IgnoredInformations ignoreAdded, bool session)
        {
            this.IgnoreAdded = ignoreAdded;
            this.Session = session;
        }

        public IgnoredAddedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(IgnoreAdded.TypeId);
            IgnoreAdded.Serialize(writer);
            writer.WriteBoolean(Session);
        }

        public override void Deserialize(IDataReader reader)
        {
            IgnoreAdded = ProtocolTypeManager.GetInstance<IgnoredInformations>(reader.ReadShort());
            IgnoreAdded.Deserialize(reader);
            Session = reader.ReadBoolean();
        }

    }
}
