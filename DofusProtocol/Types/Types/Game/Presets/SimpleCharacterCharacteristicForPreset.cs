namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SimpleCharacterCharacteristicForPreset
    {
        public const short Id  = 541;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public string Keyword { get; set; }
        public short @base { get; set; }
        public short Additionnal { get; set; }

        public SimpleCharacterCharacteristicForPreset(string keyword, short @base, short additionnal)
        {
            this.Keyword = keyword;
            this.@base = @base;
            this.Additionnal = additionnal;
        }

        public SimpleCharacterCharacteristicForPreset() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Keyword);
            writer.WriteVarShort(@base);
            writer.WriteVarShort(Additionnal);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            Keyword = reader.ReadUTF();
            @base = reader.ReadVarShort();
            Additionnal = reader.ReadVarShort();
        }

    }
}
