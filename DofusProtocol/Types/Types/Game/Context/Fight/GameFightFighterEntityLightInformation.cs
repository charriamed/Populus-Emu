namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightFighterEntityLightInformation : GameFightFighterLightInformations
    {
        public new const short Id = 548;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte EntityModelId { get; set; }
        public double MasterId { get; set; }

        public GameFightFighterEntityLightInformation(bool sex, bool alive, double objectId, sbyte wave, ushort level, sbyte breed, sbyte entityModelId, double masterId)
        {
            this.Sex = sex;
            this.Alive = alive;
            this.ObjectId = objectId;
            this.Wave = wave;
            this.Level = level;
            this.Breed = breed;
            this.EntityModelId = entityModelId;
            this.MasterId = masterId;
        }

        public GameFightFighterEntityLightInformation() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(EntityModelId);
            writer.WriteDouble(MasterId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            EntityModelId = reader.ReadSByte();
            MasterId = reader.ReadDouble();
        }

    }
}
