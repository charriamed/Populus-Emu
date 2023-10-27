namespace Stump.DofusProtocol.Messages
{
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [System.Serializable]
    public class IdentificationFailedForBadVersionMessage : IdentificationFailedMessage
    {
        public new const uint Id = 21;
        public override uint MessageId
        {
            get { return Id; }
        }
        public Version RequiredVersion { get; set; }

        public IdentificationFailedForBadVersionMessage(sbyte reason, Version requiredVersion)
        {
            this.Reason = reason;
            this.RequiredVersion = requiredVersion;
        }

        public IdentificationFailedForBadVersionMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            RequiredVersion.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            RequiredVersion = new Version();
            RequiredVersion.Deserialize(reader);
        }

    }
}
