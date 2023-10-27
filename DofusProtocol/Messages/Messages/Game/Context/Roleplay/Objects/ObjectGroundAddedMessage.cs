namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectGroundAddedMessage : Message
    {
        public const uint Id = 3017;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort CellId { get; set; }
        public ushort ObjectGID { get; set; }

        public ObjectGroundAddedMessage(ushort cellId, ushort objectGID)
        {
            this.CellId = cellId;
            this.ObjectGID = objectGID;
        }

        public ObjectGroundAddedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(CellId);
            writer.WriteVarUShort(ObjectGID);
        }

        public override void Deserialize(IDataReader reader)
        {
            CellId = reader.ReadVarUShort();
            ObjectGID = reader.ReadVarUShort();
        }

    }
}
