namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PaddockMoveItemRequestMessage : Message
    {
        public const uint Id = 6052;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort OldCellId { get; set; }
        public ushort NewCellId { get; set; }

        public PaddockMoveItemRequestMessage(ushort oldCellId, ushort newCellId)
        {
            this.OldCellId = oldCellId;
            this.NewCellId = newCellId;
        }

        public PaddockMoveItemRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(OldCellId);
            writer.WriteVarUShort(NewCellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            OldCellId = reader.ReadVarUShort();
            NewCellId = reader.ReadVarUShort();
        }

    }
}
