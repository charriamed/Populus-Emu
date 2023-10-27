namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ShortcutSpell : Shortcut
    {
        public new const short Id = 368;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort SpellId { get; set; }

        public ShortcutSpell(sbyte slot, ushort spellId)
        {
            this.Slot = slot;
            this.SpellId = spellId;
        }

        public ShortcutSpell() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(SpellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            SpellId = reader.ReadVarUShort();
        }

    }
}
