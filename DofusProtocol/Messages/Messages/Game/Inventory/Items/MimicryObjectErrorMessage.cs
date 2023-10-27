namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MimicryObjectErrorMessage : SymbioticObjectErrorMessage
    {
        public new const uint Id = 6461;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Preview { get; set; }

        public MimicryObjectErrorMessage(sbyte reason, sbyte errorCode, bool preview)
        {
            this.Reason = reason;
            this.ErrorCode = errorCode;
            this.Preview = preview;
        }

        public MimicryObjectErrorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(Preview);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Preview = reader.ReadBoolean();
        }

    }
}
