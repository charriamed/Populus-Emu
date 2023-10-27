namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectGroundRemovedMessage : Message
    {
        public const uint Id = 3014;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort Cell { get; set; }

        public ObjectGroundRemovedMessage(ushort cell)
        {
            this.Cell = cell;
        }

        public ObjectGroundRemovedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(Cell);
        }

        public override void Deserialize(IDataReader reader)
        {
            Cell = reader.ReadVarUShort();
        }

    }
}
