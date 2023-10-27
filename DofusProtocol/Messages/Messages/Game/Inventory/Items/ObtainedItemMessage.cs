namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObtainedItemMessage : Message
    {
        public const uint Id = 6519;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort GenericId { get; set; }
        public uint BaseQuantity { get; set; }

        public ObtainedItemMessage(ushort genericId, uint baseQuantity)
        {
            this.GenericId = genericId;
            this.BaseQuantity = baseQuantity;
        }

        public ObtainedItemMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(GenericId);
            writer.WriteVarUInt(BaseQuantity);
        }

        public override void Deserialize(IDataReader reader)
        {
            GenericId = reader.ReadVarUShort();
            BaseQuantity = reader.ReadVarUInt();
        }

    }
}
