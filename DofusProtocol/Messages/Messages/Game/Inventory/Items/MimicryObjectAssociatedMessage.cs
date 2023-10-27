namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MimicryObjectAssociatedMessage : SymbioticObjectAssociatedMessage
    {
        public new const uint Id = 6462;
        public override uint MessageId
        {
            get { return Id; }
        }

        public MimicryObjectAssociatedMessage(uint hostUID)
        {
            this.HostUID = hostUID;
        }

        public MimicryObjectAssociatedMessage() { }

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
