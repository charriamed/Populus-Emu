namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class DareInformationsMessage : Message
    {
        public const uint Id = 6656;
        public override uint MessageId
        {
            get { return Id; }
        }
        public DareInformations DareFixedInfos { get; set; }
        public DareVersatileInformations DareVersatilesInfos { get; set; }

        public DareInformationsMessage(DareInformations dareFixedInfos, DareVersatileInformations dareVersatilesInfos)
        {
            this.DareFixedInfos = dareFixedInfos;
            this.DareVersatilesInfos = dareVersatilesInfos;
        }

        public DareInformationsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            DareFixedInfos.Serialize(writer);
            DareVersatilesInfos.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            DareFixedInfos = new DareInformations();
            DareFixedInfos.Deserialize(reader);
            DareVersatilesInfos = new DareVersatileInformations();
            DareVersatilesInfos.Deserialize(reader);
        }

    }
}
