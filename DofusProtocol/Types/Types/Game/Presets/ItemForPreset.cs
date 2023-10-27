namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ItemForPreset
    {
        public const short Id  = 540;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public short Position { get; set; }
        public ushort ObjGid { get; set; }
        public uint ObjUid { get; set; }

        public ItemForPreset(short position, ushort objGid, uint objUid)
        {
            this.Position = position;
            this.ObjGid = objGid;
            this.ObjUid = objUid;
        }

        public ItemForPreset() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort(Position);
            writer.WriteVarUShort(ObjGid);
            writer.WriteVarUInt(ObjUid);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            Position = reader.ReadShort();
            ObjGid = reader.ReadVarUShort();
            ObjUid = reader.ReadVarUInt();
        }

    }
}
