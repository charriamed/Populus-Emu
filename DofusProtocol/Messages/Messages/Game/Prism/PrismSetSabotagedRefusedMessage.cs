namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PrismSetSabotagedRefusedMessage : Message
    {
        public const uint Id = 6466;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort SubAreaId { get; set; }
        public sbyte Reason { get; set; }

        public PrismSetSabotagedRefusedMessage(ushort subAreaId, sbyte reason)
        {
            this.SubAreaId = subAreaId;
            this.Reason = reason;
        }

        public PrismSetSabotagedRefusedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SubAreaId);
            writer.WriteSByte(Reason);
        }

        public override void Deserialize(IDataReader reader)
        {
            SubAreaId = reader.ReadVarUShort();
            Reason = reader.ReadSByte();
        }

    }
}
