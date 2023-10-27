namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterMinimalAllianceInformations : CharacterMinimalGuildInformations
    {
        public new const short Id = 444;
        public override short TypeId
        {
            get { return Id; }
        }
        public BasicAllianceInformations Alliance { get; set; }

        public CharacterMinimalAllianceInformations(ulong objectId, string name, ushort level, EntityLook entityLook, sbyte breed, BasicGuildInformations guild, BasicAllianceInformations alliance)
        {
            this.ObjectId = objectId;
            this.Name = name;
            this.Level = level;
            this.EntityLook = entityLook;
            this.Breed = breed;
            this.Guild = guild;
            this.Alliance = alliance;
        }

        public CharacterMinimalAllianceInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            Alliance.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Alliance = new BasicAllianceInformations();
            Alliance.Deserialize(reader);
        }

    }
}
