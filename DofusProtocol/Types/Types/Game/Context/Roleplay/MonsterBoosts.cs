namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MonsterBoosts
    {
        public const short Id  = 497;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public uint ObjectId { get; set; }
        public ushort XpBoost { get; set; }
        public ushort DropBoost { get; set; }

        public MonsterBoosts(uint objectId, ushort xpBoost, ushort dropBoost)
        {
            this.ObjectId = objectId;
            this.XpBoost = xpBoost;
            this.DropBoost = dropBoost;
        }

        public MonsterBoosts() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ObjectId);
            writer.WriteVarUShort(XpBoost);
            writer.WriteVarUShort(DropBoost);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadVarUInt();
            XpBoost = reader.ReadVarUShort();
            DropBoost = reader.ReadVarUShort();
        }

    }
}
