namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectUseOnCellMessage : ObjectUseMessage
    {
        public new const uint Id = 3013;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort Cells { get; set; }

        public ObjectUseOnCellMessage(uint objectUID, ushort cells)
        {
            this.ObjectUID = objectUID;
            this.Cells = cells;
        }

        public ObjectUseOnCellMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(Cells);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Cells = reader.ReadVarUShort();
        }

    }
}
