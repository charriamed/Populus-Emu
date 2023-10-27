namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightFighterTaxCollectorLightInformations : GameFightFighterLightInformations
    {
        public new const short Id = 457;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort FirstNameId { get; set; }
        public ushort LastNameId { get; set; }

        public GameFightFighterTaxCollectorLightInformations(bool sex, bool alive, double objectId, sbyte wave, ushort level, sbyte breed, ushort firstNameId, ushort lastNameId)
        {
            this.Sex = sex;
            this.Alive = alive;
            this.ObjectId = objectId;
            this.Wave = wave;
            this.Level = level;
            this.Breed = breed;
            this.FirstNameId = firstNameId;
            this.LastNameId = lastNameId;
        }

        public GameFightFighterTaxCollectorLightInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(FirstNameId);
            writer.WriteVarUShort(LastNameId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            FirstNameId = reader.ReadVarUShort();
            LastNameId = reader.ReadVarUShort();
        }

    }
}
