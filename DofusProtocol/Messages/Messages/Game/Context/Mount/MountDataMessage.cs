namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MountDataMessage : Message
    {
        public const uint Id = 5973;
        public override uint MessageId
        {
            get { return Id; }
        }
        public MountClientData MountData { get; set; }

        public MountDataMessage(MountClientData mountData)
        {
            this.MountData = mountData;
        }

        public MountDataMessage() { }

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
