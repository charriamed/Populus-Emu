namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterBasicMinimalInformations : AbstractCharacterInformation
    {
        public new const short Id = 503;
        public override short TypeId
        {
            get { return Id; }
        }
        public string Name { get; set; }

        public CharacterBasicMinimalInformations(ulong objectId, string name)
        {
            this.ObjectId = objectId;
            this.Name = name;
        }

        public CharacterBasicMinimalInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(Name);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Name = reader.ReadUTF();
        }

    }
}
