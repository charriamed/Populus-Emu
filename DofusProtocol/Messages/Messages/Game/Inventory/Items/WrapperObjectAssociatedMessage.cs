namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class WrapperObjectAssociatedMessage : SymbioticObjectAssociatedMessage
    {
        public new const uint Id = 6523;
        public override uint MessageId
        {
            get { return Id; }
        }

        public WrapperObjectAssociatedMessage(uint hostUID)
        {
            this.HostUID = hostUID;
        }

        public WrapperObjectAssociatedMessage() { }

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
