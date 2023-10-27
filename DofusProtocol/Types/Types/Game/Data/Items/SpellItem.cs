namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SpellItem : Item
    {
        public new const short Id = 49;
        public override short TypeId
        {
            get { return Id; }
        }
        public int SpellId { get; set; }
        public short SpellLevel { get; set; }

        public SpellItem(int spellId, short spellLevel)
        {
            this.SpellId = spellId;
            this.SpellLevel = spellLevel;
        }

        public SpellItem() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(SpellId);
            writer.WriteShort(SpellLevel);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            SpellId = reader.ReadInt();
            SpellLevel = reader.ReadShort();
        }

    }
}
