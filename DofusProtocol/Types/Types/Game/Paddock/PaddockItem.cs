namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PaddockItem : ObjectItemInRolePlay
    {
        public new const short Id = 185;
        public override short TypeId
        {
            get { return Id; }
        }
        public ItemDurability Durability { get; set; }

        public PaddockItem(ushort cellId, ushort objectGID, ItemDurability durability)
        {
            this.CellId = cellId;
            this.ObjectGID = objectGID;
            this.Durability = durability;
        }

        public PaddockItem() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            Durability.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Durability = new ItemDurability();
            Durability.Deserialize(reader);
        }

    }
}
