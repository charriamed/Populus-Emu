namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightFighterMonsterLightInformations : GameFightFighterLightInformations
    {
        public new const short Id = 455;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort CreatureGenericId { get; set; }

        public GameFightFighterMonsterLightInformations(bool sex, bool alive, double objectId, sbyte wave, ushort level, sbyte breed, ushort creatureGenericId)
        {
            this.Sex = sex;
            this.Alive = alive;
            this.ObjectId = objectId;
            this.Wave = wave;
            this.Level = level;
            this.Breed = breed;
            this.CreatureGenericId = creatureGenericId;
        }

        public GameFightFighterMonsterLightInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(CreatureGenericId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            CreatureGenericId = reader.ReadVarUShort();
        }

    }
}
