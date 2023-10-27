using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Stump.Core.IO;
namespace Stump.DofusProtocol.Types
{
    public class GameFightFighterNamedLightInformations : GameFightFighterLightInformations
    {
        public new const short Id = 456;
        public string name;
        public override short TypeId
        {
            get { return Id; }
        }
        public GameFightFighterNamedLightInformations() { }
        public GameFightFighterNamedLightInformations(double id, sbyte wave, ushort level, sbyte breed, bool sex, bool alive,
            string name) : base(id, wave, level, breed, sex, alive)
        {
            this.ObjectId = id;
            this.Wave = wave;
            this.Level = level;
            this.Breed = breed;
            this.Sex = sex;
            this.Alive = alive;
            this.name = name;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF((string)this.name);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            name = reader.ReadUTF();
        }
    }
}