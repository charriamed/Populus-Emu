namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class WrapperObjectErrorMessage : SymbioticObjectErrorMessage
    {
        public new const uint Id = 6529;
        public override uint MessageId
        {
            get { return Id; }
        }

        public WrapperObjectErrorMessage(sbyte reason, sbyte errorCode)
        {
            this.Reason = reason;
            this.ErrorCode = errorCode;
        }

        public WrapperObjectErrorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }

    }
}
