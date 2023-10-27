namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class SpellForPreset
    {
        public const short Id  = 557;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort SpellId { get; set; }
        public short[] Shortcuts { get; set; }

        public SpellForPreset(ushort spellId, short[] shortcuts)
        {
            this.SpellId = spellId;
            this.Shortcuts = shortcuts;
        }

        public SpellForPreset() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SpellId);
            writer.WriteShort((short)Shortcuts.Count());
            for (var shortcutsIndex = 0; shortcutsIndex < Shortcuts.Count(); shortcutsIndex++)
            {
                writer.WriteShort(Shortcuts[shortcutsIndex]);
            }
        }

        public virtual void Deserialize(IDataReader reader)
        {
            SpellId = reader.ReadVarUShort();
            var shortcutsCount = reader.ReadUShort();
            Shortcuts = new short[shortcutsCount];
            for (var shortcutsIndex = 0; shortcutsIndex < shortcutsCount; shortcutsIndex++)
            {
                Shortcuts[shortcutsIndex] = reader.ReadShort();
            }
        }

    }
}
