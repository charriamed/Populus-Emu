namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class DareSubscribedMessage : Message
    {
        public const uint Id = 6660;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Success { get; set; }
        public bool Subscribe { get; set; }
        public double DareId { get; set; }
        public DareVersatileInformations DareVersatilesInfos { get; set; }

        public DareSubscribedMessage(bool success, bool subscribe, double dareId, DareVersatileInformations dareVersatilesInfos)
        {
            this.Success = success;
            this.Subscribe = subscribe;
            this.DareId = dareId;
            this.DareVersatilesInfos = dareVersatilesInfos;
        }

        public DareSubscribedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, Success);
            flag = BooleanByteWrapper.SetFlag(flag, 1, Subscribe);
            writer.WriteByte(flag);
            writer.WriteDouble(DareId);
            DareVersatilesInfos.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            var flag = reader.ReadByte();
            Success = BooleanByteWrapper.GetFlag(flag, 0);
            Subscribe = BooleanByteWrapper.GetFlag(flag, 1);
            DareId = reader.ReadDouble();
            DareVersatilesInfos = new DareVersatileInformations();
            DareVersatilesInfos.Deserialize(reader);
        }

    }
}
