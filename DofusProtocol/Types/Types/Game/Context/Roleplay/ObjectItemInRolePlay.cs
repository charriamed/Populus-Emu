namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectItemInRolePlay
    {
        public const short Id  = 198;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort CellId { get; set; }
        public ushort ObjectGID { get; set; }

        public ObjectItemInRolePlay(ushort cellId, ushort objectGID)
        {
            this.CellId = cellId;
            this.ObjectGID = objectGID;
        }

        public ObjectItemInRolePlay() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(CellId);
            writer.WriteVarUShort(ObjectGID);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            CellId = reader.ReadVarUShort();
            ObjectGID = reader.ReadVarUShort();
        }

    }
}
