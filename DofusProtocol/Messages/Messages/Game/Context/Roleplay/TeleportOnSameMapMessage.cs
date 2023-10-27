namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TeleportOnSameMapMessage : Message
    {
        public const uint Id = 6048;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TargetId { get; set; }
        public ushort CellId { get; set; }

        public TeleportOnSameMapMessage(double targetId, ushort cellId)
        {
            this.TargetId = targetId;
            this.CellId = cellId;
        }

        public TeleportOnSameMapMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(TargetId);
            writer.WriteVarUShort(CellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            TargetId = reader.ReadDouble();
            CellId = reader.ReadVarUShort();
        }

    }
}
