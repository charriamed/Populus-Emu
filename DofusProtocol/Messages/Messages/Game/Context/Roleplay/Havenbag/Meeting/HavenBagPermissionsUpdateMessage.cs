namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HavenBagPermissionsUpdateMessage : Message
    {
        public const uint Id = 6713;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int Permissions { get; set; }

        public HavenBagPermissionsUpdateMessage(int permissions)
        {
            this.Permissions = permissions;
        }

        public HavenBagPermissionsUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(Permissions);
        }

        public override void Deserialize(IDataReader reader)
        {
            Permissions = reader.ReadInt();
        }

    }
}
