namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterSpellModification
    {
        public const short Id  = 215;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte ModificationType { get; set; }
        public ushort SpellId { get; set; }
        public CharacterBaseCharacteristic Value { get; set; }

        public CharacterSpellModification(sbyte modificationType, ushort spellId, CharacterBaseCharacteristic value)
        {
            this.ModificationType = modificationType;
            this.SpellId = spellId;
            this.Value = value;
        }

        public CharacterSpellModification() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(ModificationType);
            writer.WriteVarUShort(SpellId);
            Value.Serialize(writer);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ModificationType = reader.ReadSByte();
            SpellId = reader.ReadVarUShort();
            Value = new CharacterBaseCharacteristic();
            Value.Deserialize(reader);
        }

    }
}
