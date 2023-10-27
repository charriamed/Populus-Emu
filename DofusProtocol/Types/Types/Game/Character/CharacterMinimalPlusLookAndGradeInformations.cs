namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterMinimalPlusLookAndGradeInformations : CharacterMinimalPlusLookInformations
    {
        public new const short Id = 193;
        public override short TypeId
        {
            get { return Id; }
        }
        public uint Grade { get; set; }

        public CharacterMinimalPlusLookAndGradeInformations(ulong objectId, string name, ushort level, EntityLook entityLook, sbyte breed, uint grade)
        {
            this.ObjectId = objectId;
            this.Name = name;
            this.Level = level;
            this.EntityLook = entityLook;
            this.Breed = breed;
            this.Grade = grade;
        }

        public CharacterMinimalPlusLookAndGradeInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(Grade);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Grade = reader.ReadVarUInt();
        }

    }
}
