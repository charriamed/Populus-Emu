namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ShortcutObjectItem : ShortcutObject
    {
        public new const short Id = 371;
        public override short TypeId
        {
            get { return Id; }
        }
        public int ItemUID { get; set; }
        public int ItemGID { get; set; }

        public ShortcutObjectItem(sbyte slot, int itemUID, int itemGID)
        {
            this.Slot = slot;
            this.ItemUID = itemUID;
            this.ItemGID = itemGID;
        }

        public ShortcutObjectItem() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(ItemUID);
            writer.WriteInt(ItemGID);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ItemUID = reader.ReadInt();
            ItemGID = reader.ReadInt();
        }

    }
}
