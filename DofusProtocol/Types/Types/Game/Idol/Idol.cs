namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class Idol
    {
        public const short Id  = 489;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort ObjectId { get; set; }
        public ushort XpBonusPercent { get; set; }
        public ushort DropBonusPercent { get; set; }

        public Idol(ushort objectId, ushort xpBonusPercent, ushort dropBonusPercent)
        {
            this.ObjectId = objectId;
            this.XpBonusPercent = xpBonusPercent;
            this.DropBonusPercent = dropBonusPercent;
        }

        public Idol() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ObjectId);
            writer.WriteVarUShort(XpBonusPercent);
            writer.WriteVarUShort(DropBonusPercent);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadVarUShort();
            XpBonusPercent = reader.ReadVarUShort();
            DropBonusPercent = reader.ReadVarUShort();
        }

    }
}
