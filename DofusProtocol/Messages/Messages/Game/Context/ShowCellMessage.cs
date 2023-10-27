namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ShowCellMessage : Message
    {
        public const uint Id = 5612;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double SourceId { get; set; }
        public ushort CellId { get; set; }

        public ShowCellMessage(double sourceId, ushort cellId)
        {
            this.SourceId = sourceId;
            this.CellId = cellId;
        }

        public ShowCellMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(SourceId);
            writer.WriteVarUShort(CellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            SourceId = reader.ReadDouble();
            CellId = reader.ReadVarUShort();
        }

    }
}
