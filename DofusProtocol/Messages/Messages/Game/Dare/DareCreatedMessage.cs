namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class DareCreatedMessage : Message
    {
        public const uint Id = 6668;
        public override uint MessageId
        {
            get { return Id; }
        }
        public DareInformations DareInfos { get; set; }
        public bool NeedNotifications { get; set; }

        public DareCreatedMessage(DareInformations dareInfos, bool needNotifications)
        {
            this.DareInfos = dareInfos;
            this.NeedNotifications = needNotifications;
        }

        public DareCreatedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            DareInfos.Serialize(writer);
            writer.WriteBoolean(NeedNotifications);
        }

        public override void Deserialize(IDataReader reader)
        {
            DareInfos = new DareInformations();
            DareInfos.Deserialize(reader);
            NeedNotifications = reader.ReadBoolean();
        }

    }
}
