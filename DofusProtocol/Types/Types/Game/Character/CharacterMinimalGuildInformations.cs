namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterMinimalGuildInformations : CharacterMinimalPlusLookInformations
    {
        public new const short Id = 445;
        public override short TypeId
        {
            get { return Id; }
        }
        public BasicGuildInformations Guild { get; set; }

        public CharacterMinimalGuildInformations(ulong objectId, string name, ushort level, EntityLook entityLook, sbyte breed, BasicGuildInformations guild)
        {
            this.ObjectId = objectId;
            this.Name = name;
            this.Level = level;
            this.EntityLook = entityLook;
            this.Breed = breed;
            this.Guild = guild;
        }

        public CharacterMinimalGuildInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            Guild.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Guild = new BasicGuildInformations();
            Guild.Deserialize(reader);
        }

    }
}
