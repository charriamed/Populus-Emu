namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SymbioticObjectErrorMessage : ObjectErrorMessage
    {
        public new const uint Id = 6526;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte ErrorCode { get; set; }

        public SymbioticObjectErrorMessage(sbyte reason, sbyte errorCode)
        {
            this.Reason = reason;
            this.ErrorCode = errorCode;
        }

        public SymbioticObjectErrorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(ErrorCode);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ErrorCode = reader.ReadSByte();
        }

    }
}
