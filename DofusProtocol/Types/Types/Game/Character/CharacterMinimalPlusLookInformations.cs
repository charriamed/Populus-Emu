namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterMinimalPlusLookInformations : CharacterMinimalInformations
    {
        public new const short Id = 163;
        public override short TypeId
        {
            get { return Id; }
        }
        public EntityLook EntityLook { get; set; }
        public sbyte Breed { get; set; }

        public CharacterMinimalPlusLookInformations(ulong objectId, string name, ushort level, EntityLook entityLook, sbyte breed)
        {
            this.ObjectId = objectId;
            this.Name = name;
            this.Level = level;
            this.EntityLook = entityLook;
            this.Breed = breed;
        }

        public CharacterMinimalPlusLookInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            EntityLook.Serialize(writer);
            writer.WriteSByte(Breed);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            EntityLook = new EntityLook();
            EntityLook.Deserialize(reader);
            Breed = reader.ReadSByte();
        }

    }
}
