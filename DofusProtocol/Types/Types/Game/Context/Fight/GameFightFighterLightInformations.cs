using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Stump.Core.IO;
namespace Stump.DofusProtocol.Types
{
    public class GameFightFighterLightInformations
    {
        public const short Id = 413;
        public double ObjectId;
        public sbyte Wave;
        public ushort Level;
        public sbyte Breed;
        public bool Sex;
        public bool Alive;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public GameFightFighterLightInformations() { }
        public GameFightFighterLightInformations(double id, sbyte wave, ushort level, sbyte breed, bool sex, bool alive)
        {
            this.ObjectId = id;
            this.Wave = wave;
            this.Level = level;
            this.Breed = breed;
            this.Sex = sex;
            this.Alive = alive;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, Sex);
            flag = BooleanByteWrapper.SetFlag(flag, 1, Alive);
            writer.WriteByte(flag);
            writer.WriteDouble((double)this.ObjectId);
            writer.WriteSByte(this.Wave);
            writer.WriteVarUShort(this.Level);
            writer.WriteSByte(this.Breed);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            var flag = reader.ReadByte();
            Sex = BooleanByteWrapper.GetFlag(flag, 0);
            Alive = BooleanByteWrapper.GetFlag(flag, 1);
            ObjectId = reader.ReadDouble();
            Wave = reader.ReadSByte();
            Level = reader.ReadVarUShort();
            Breed = reader.ReadSByte();
        }
    }
}