namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectEffectMount : ObjectEffect
    {
        public new const short Id = 179;
        public override short TypeId
        {
            get { return Id; }
        }
        public bool Sex { get; set; }
        public bool IsRideable { get; set; }
        public bool IsFeconded { get; set; }
        public bool IsFecondationReady { get; set; }
        public ulong ObjectId { get; set; }
        public ulong ExpirationDate { get; set; }
        public uint Model { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public sbyte Level { get; set; }
        public int ReproductionCount { get; set; }
        public uint ReproductionCountMax { get; set; }
        public ObjectEffectInteger[] Effects { get; set; }
        public uint[] Capacities { get; set; }

        public ObjectEffectMount(ushort actionId, bool sex, bool isRideable, bool isFeconded, bool isFecondationReady, ulong objectId, ulong expirationDate, uint model, string name, string owner, sbyte level, int reproductionCount, uint reproductionCountMax, ObjectEffectInteger[] effects, uint[] capacities)
        {
            this.ActionId = actionId;
            this.Sex = sex;
            this.IsRideable = isRideable;
            this.IsFeconded = isFeconded;
            this.IsFecondationReady = isFecondationReady;
            this.ObjectId = objectId;
            this.ExpirationDate = expirationDate;
            this.Model = model;
            this.Name = name;
            this.Owner = owner;
            this.Level = level;
            this.ReproductionCount = reproductionCount;
            this.ReproductionCountMax = reproductionCountMax;
            this.Effects = effects;
            this.Capacities = capacities;
        }

        public ObjectEffectMount() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, Sex);
            flag = BooleanByteWrapper.SetFlag(flag, 1, IsRideable);
            flag = BooleanByteWrapper.SetFlag(flag, 2, IsFeconded);
            flag = BooleanByteWrapper.SetFlag(flag, 3, IsFecondationReady);
            writer.WriteByte(flag);
            writer.WriteVarULong(ObjectId);
            writer.WriteVarULong(ExpirationDate);
            writer.WriteVarUInt(Model);
            writer.WriteUTF(Name);
            writer.WriteUTF(Owner);
            writer.WriteSByte(Level);
            writer.WriteVarInt(ReproductionCount);
            writer.WriteVarUInt(ReproductionCountMax);
            writer.WriteShort((short)Effects.Count());
            for (var effectsIndex = 0; effectsIndex < Effects.Count(); effectsIndex++)
            {
                var objectToSend = Effects[effectsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)Capacities.Count());
            for (var capacitiesIndex = 0; capacitiesIndex < Capacities.Count(); capacitiesIndex++)
            {
                writer.WriteVarUInt(Capacities[capacitiesIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var flag = reader.ReadByte();
            Sex = BooleanByteWrapper.GetFlag(flag, 0);
            IsRideable = BooleanByteWrapper.GetFlag(flag, 1);
            IsFeconded = BooleanByteWrapper.GetFlag(flag, 2);
            IsFecondationReady = BooleanByteWrapper.GetFlag(flag, 3);
            ObjectId = reader.ReadVarULong();
            ExpirationDate = reader.ReadVarULong();
            Model = reader.ReadVarUInt();
            Name = reader.ReadUTF();
            Owner = reader.ReadUTF();
            Level = reader.ReadSByte();
            ReproductionCount = reader.ReadVarInt();
            ReproductionCountMax = reader.ReadVarUInt();
            var effectsCount = reader.ReadUShort();
            Effects = new ObjectEffectInteger[effectsCount];
            for (var effectsIndex = 0; effectsIndex < effectsCount; effectsIndex++)
            {
                var objectToAdd = new ObjectEffectInteger();
                objectToAdd.Deserialize(reader);
                Effects[effectsIndex] = objectToAdd;
            }
            var capacitiesCount = reader.ReadUShort();
            Capacities = new uint[capacitiesCount];
            for (var capacitiesIndex = 0; capacitiesIndex < capacitiesCount; capacitiesIndex++)
            {
                Capacities[capacitiesIndex] = reader.ReadVarUInt();
            }
        }

    }
}
