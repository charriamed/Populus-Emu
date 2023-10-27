namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MountSetMessage : Message
    {
        public const uint Id = 5968;
        public override uint MessageId
        {
            get { return Id; }
        }
        public MountClientData MountData { get; set; }

        public MountSetMessage(MountClientData mountData)
        {
            this.MountData = mountData;
        }

        public MountSetMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            MountData.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            MountData = new MountClientData();
            MountData.Deserialize(reader);
        }

    }
}
