namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterBaseCharacteristic
    {
        public const short Id  = 4;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public short @base { get; set; }
        public short Additionnal { get; set; }
        public short ObjectsAndMountBonus { get; set; }
        public short AlignGiftBonus { get; set; }
        public short ContextModif { get; set; }

        public CharacterBaseCharacteristic(short @base, short additionnal, short objectsAndMountBonus, short alignGiftBonus, short contextModif)
        {
            this.@base = @base;
            this.Additionnal = additionnal;
            this.ObjectsAndMountBonus = objectsAndMountBonus;
            this.AlignGiftBonus = alignGiftBonus;
            this.ContextModif = contextModif;
        }

        public CharacterBaseCharacteristic() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(@base);
            writer.WriteVarShort(Additionnal);
            writer.WriteVarShort(ObjectsAndMountBonus);
            writer.WriteVarShort(AlignGiftBonus);
            writer.WriteVarShort(ContextModif);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            @base = reader.ReadVarShort();
            Additionnal = reader.ReadVarShort();
            ObjectsAndMountBonus = reader.ReadVarShort();
            AlignGiftBonus = reader.ReadVarShort();
            ContextModif = reader.ReadVarShort();
        }

    }
}
