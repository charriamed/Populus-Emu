namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SymbioticObjectAssociatedMessage : Message
    {
        public const uint Id = 6527;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint HostUID { get; set; }

        public SymbioticObjectAssociatedMessage(uint hostUID)
        {
            this.HostUID = hostUID;
        }

        public SymbioticObjectAssociatedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(HostUID);
        }

        public override void Deserialize(IDataReader reader)
        {
            HostUID = reader.ReadVarUInt();
        }

    }
}
