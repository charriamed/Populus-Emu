namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterBaseInformations : CharacterMinimalPlusLookInformations
    {
        public new const short Id = 45;
        public override short TypeId
        {
            get { return Id; }
        }
        public bool Sex { get; set; }

        public CharacterBaseInformations(ulong objectId, string name, ushort level, EntityLook entityLook, sbyte breed, bool sex)
        {
            this.ObjectId = objectId;
            this.Name = name;
            this.Level = level;
            this.EntityLook = entityLook;
            this.Breed = breed;
            this.Sex = sex;
        }

        public CharacterBaseInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(Sex);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Sex = reader.ReadBoolean();
        }

    }
}
