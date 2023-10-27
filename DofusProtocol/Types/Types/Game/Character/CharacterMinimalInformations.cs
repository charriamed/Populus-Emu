namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterMinimalInformations : CharacterBasicMinimalInformations
    {
        public new const short Id = 110;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort Level { get; set; }

        public CharacterMinimalInformations(ulong objectId, string name, ushort level)
        {
            this.ObjectId = objectId;
            this.Name = name;
            this.Level = level;
        }

        public CharacterMinimalInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(Level);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Level = reader.ReadVarUShort();
        }

    }
}
