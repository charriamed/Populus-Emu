namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ProtocolRequired : Message
    {
        public const uint Id = 1;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int RequiredVersion { get; set; }
        public int CurrentVersion { get; set; }

        public ProtocolRequired(int requiredVersion, int currentVersion)
        {
            this.RequiredVersion = requiredVersion;
            this.CurrentVersion = currentVersion;
        }

        public ProtocolRequired() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(RequiredVersion);
            writer.WriteInt(CurrentVersion);
        }

        public override void Deserialize(IDataReader reader)
        {
            RequiredVersion = reader.ReadInt();
            CurrentVersion = reader.ReadInt();
        }

    }
}
