namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PrismFightSwapRequestMessage : Message
    {
        public const uint Id = 5901;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort SubAreaId { get; set; }
        public ulong TargetId { get; set; }

        public PrismFightSwapRequestMessage(ushort subAreaId, ulong targetId)
        {
            this.SubAreaId = subAreaId;
            this.TargetId = targetId;
        }

        public PrismFightSwapRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SubAreaId);
            writer.WriteVarULong(TargetId);
        }

        public override void Deserialize(IDataReader reader)
        {
            SubAreaId = reader.ReadVarUShort();
            TargetId = reader.ReadVarULong();
        }

    }
}
