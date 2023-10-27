namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterCharacteristicForPreset : SimpleCharacterCharacteristicForPreset
    {
        public new const short Id = 539;
        public override short TypeId
        {
            get { return Id; }
        }
        public short Stuff { get; set; }

        public CharacterCharacteristicForPreset(string keyword, short @base, short additionnal, short stuff)
        {
            this.Keyword = keyword;
            this.@base = @base;
            this.Additionnal = additionnal;
            this.Stuff = stuff;
        }

        public CharacterCharacteristicForPreset() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarShort(Stuff);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Stuff = reader.ReadVarShort();
        }

    }
}
