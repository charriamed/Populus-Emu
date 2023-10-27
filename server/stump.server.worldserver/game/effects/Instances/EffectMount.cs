using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Text;
using Stump.Core.Extensions;
using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;

namespace Stump.Server.WorldServer.Game.Effects.Instances
{
    [Serializable]
    public class EffectMount : EffectBase
    {
        public double id;
        public double expirationDate;
        public uint model;
        public String name = "";
        public String owner = "";
        public uint level = 0;
        public Boolean sex = false;
        public Boolean isRideable = false;
        public Boolean isFeconded = false;
        public Boolean isFecondationReady = false;
        public int reproductionCount = 0;
        public uint reproductionCountMax = 0;
        public List<EffectInteger> effects;
        public List<uint> capacities;

        public EffectMount()
        {

        }

        public EffectMount(EffectMount copy)
            : this(copy.EffectId, copy.Id, copy.expirationDate, copy.model, copy.name, copy.owner, copy.level, copy.sex, copy.isRideable, copy.isFeconded, copy.isFecondationReady, copy.reproductionCount, copy.reproductionCountMax, copy.effects, copy.capacities, copy)
        {

        }

        public EffectMount(EffectsEnum effectId, double id, double expirationDate, uint model, string name, string owner, uint level, bool sex, bool isRideable, bool isFeconded, bool isFecondationReady, int reproductionCount, uint reproductionCountMax, List<EffectInteger> effects, List<uint> capacities, EffectBase effect)
            : base((short)effectId, effect)
        {
            this.id = id;
            this.expirationDate = expirationDate;
            this.model = model;
            this.name = name;
            this.owner = owner;
            this.level = level;
            this.sex = sex;
            this.isRideable = isRideable;
            this.isFeconded = isFeconded;
            this.isFecondationReady = isFecondationReady;
            this.reproductionCount = reproductionCount;
            this.reproductionCountMax = reproductionCountMax;
            this.effects = effects;
            this.capacities = capacities;
        }

        public EffectMount(EffectsEnum effectId, double id, double expirationDate, uint model, string name, string owner, uint level, bool sex, bool isRideable, bool isFeconded, bool isFecondationReady, int reproductionCount, uint reproductionCountMax, List<EffectInteger> effects, List<uint> capacities)
    : base((short)effectId, new EffectBase())
        {
            this.id = id;
            this.expirationDate = expirationDate;
            this.model = model;
            this.name = name;
            this.owner = owner;
            this.level = level;
            this.sex = sex;
            this.isRideable = isRideable;
            this.isFeconded = isFeconded;
            this.isFecondationReady = isFecondationReady;
            this.reproductionCount = reproductionCount;
            this.reproductionCountMax = reproductionCountMax;
            this.effects = effects;
            this.capacities = capacities;
        }


        public EffectMount(EffectInstanceMount effect)
            : base(effect)
        {
            this.id = effect.id;
            this.expirationDate = effect.expirationDate;
            this.model = effect.model;
            this.name = effect.name;
            this.owner = effect.owner;
            this.level = effect.level;
            this.sex = effect.sex;
            this.isRideable = effect.isRideable;
            this.isFeconded = effect.isFeconded;
            this.isFecondationReady = effect.isFecondationReady;
            this.reproductionCount = effect.reproductionCount;
            this.reproductionCountMax = effect.reproductionCountMax;
            this.effects = effect.effects.Select(x => new EffectInteger(x)).ToList();
            this.capacities = effect.capacities;
        }

        public override int ProtocoleId => 179;

        public override byte SerializationIdenfitier => 9;

        public override object[] GetValues()
        {
            return new object[] { id, expirationDate, model, name, owner, level, sex, isRideable, isFeconded, isFecondationReady, reproductionCount, reproductionCountMax, effects, capacities };
        }

        public override ObjectEffect GetObjectEffect()
        {
            return new ObjectEffectMount((ushort)EffectId, sex, isRideable, isFeconded, isFecondationReady, (ulong)id, (ulong)expirationDate, model, name, owner, (sbyte)level, reproductionCount, reproductionCountMax, effects.Select(x => (ObjectEffectInteger)x.GetObjectEffect()).ToArray(), capacities.ToArray());
        }
        public override EffectInstance GetEffectInstance()
        {
            return new EffectInstanceMount()
            {
                effectId = (uint)EffectId,
                targetMask = TargetMask,
                delay = Delay,
                duration = Duration,
                group = Group,
                random = Random,
                modificator = Modificator,
                trigger = Trigger,
                triggers = Triggers,
                zoneMinSize = ZoneMinSize,
                zoneSize = ZoneSize,
                zoneShape = (uint)ZoneShape,
                zoneEfficiencyPercent = ZoneEfficiencyPercent,
                zoneMaxEfficiency = ZoneMaxEfficiency,
                id = this.id,
                expirationDate = this.expirationDate,
                model = this.model,
                name = this.name,
                owner = this.owner,
                level = this.level,
                sex = this.sex,
                isRideable = this.isRideable,
                isFecondationReady = this.isFecondationReady,
                reproductionCount = this.reproductionCount,
                reproductionCountMax = this.reproductionCountMax,
                effects = this.effects.Select(x => (EffectInstanceInteger)x.GetEffectInstance()).ToList(),
                capacities = this.capacities
            };
        }

        public override EffectBase GenerateEffect(EffectGenerationContext context, EffectGenerationType type = EffectGenerationType.Normal)
        {
            return new EffectMount(this);
        }
        protected override void InternalSerialize(ref System.IO.BinaryWriter writer)
        {
            base.InternalSerialize(ref writer);

            writer.Write(id);
            writer.Write(expirationDate);
            writer.Write(model);
            writer.Write(name);
            writer.Write(owner);
            writer.Write(level);
            writer.Write(sex);
            writer.Write(isRideable);
            writer.Write(isFecondationReady);
            writer.Write(reproductionCount);
            writer.Write(reproductionCountMax);

            var bin = effects.ToBinary();
            writer.Write(bin.Length);
            writer.Write(bin);

            writer.Write(capacities.Count);
            foreach (var capacity in capacities)
                writer.Write(capacity);
        }

        protected override void InternalDeserialize(ref System.IO.BinaryReader reader)
        {
            base.InternalDeserialize(ref reader);

            id = reader.ReadDouble();
            expirationDate = reader.ReadDouble();
            model = reader.ReadUInt32();
            name = reader.ReadString();
            owner = reader.ReadString();
            level = reader.ReadUInt32();
            sex = reader.ReadBoolean();
            isRideable = reader.ReadBoolean();
            isFecondationReady = reader.ReadBoolean();
            reproductionCount = reader.ReadInt32();
            reproductionCountMax = reader.ReadUInt32();

            effects = new List<EffectInteger>();
            var effectsLength = reader.ReadInt32();
            var effectsBin = new byte[effectsLength];
            effectsBin = reader.ReadBytes(effectsLength);
            effects = effectsBin.ToObject<List<EffectInteger>>();

            capacities = new List<uint>();

            var capacitiesLength = reader.ReadUInt32();
            for (var i = 0; i < capacitiesLength; i++)
                capacities.Add(reader.ReadUInt32());

        }

        public override bool Equals(object obj)
        {
            if (!(obj is EffectMount))
                return false;
            var b = obj as EffectMount;
            return base.Equals(obj);
        }

        public static bool operator ==(EffectMount a, EffectMount b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (((object)a == null) || ((object)b == null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EffectMount a, EffectMount b)
        {
            return !(a == b);
        }

        public bool Equals(EffectMount other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other);
        }

        public DateTime Date
        {
            get { return ((long)expirationDate).FromUnixTimeMs(); }
            set { expirationDate = value.GetUnixTimeStampLong(); }
        }

        public int MountId
        {
            get { return (int)id; }
            set { id = (short)value; }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = base.GetHashCode();
                return result;
            }
        }
    }
}