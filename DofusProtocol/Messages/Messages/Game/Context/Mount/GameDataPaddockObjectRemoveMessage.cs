namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameDataPaddockObjectRemoveMessage : Message
    {
        public const uint Id = 5993;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort CellId { get; set; }

        public GameDataPaddockObjectRemoveMessage(ushort cellId)
        {
            this.CellId = cellId;
        }

        public GameDataPaddockObjectRemoveMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(CellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            CellId = reader.ReadVarUShort();
        }

    }
}
