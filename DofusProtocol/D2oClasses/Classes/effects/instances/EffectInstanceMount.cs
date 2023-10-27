using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("EffectInstanceMount", "com.ankamagames.dofus.datacenter.effects.instances")]
    [Serializable]
    public class EffectInstanceMount : EffectInstance
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
        public List<EffectInstanceInteger> effects;
        public List<uint> capacities;
        [D2OIgnore]
        public double Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        [D2OIgnore]
        public double ExpirationDate
        {
            get { return this.expirationDate; }
            set { this.expirationDate = value; }
        }
        [D2OIgnore]
        public uint Model
        {
            get { return this.model; }
            set { this.model = value; }
        }
        [D2OIgnore]
        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        [D2OIgnore]
        public String Owner
        {
            get { return this.owner; }
            set { this.owner = value; }
        }
        [D2OIgnore]
        public uint Level
        {
            get { return this.level; }
            set { this.level = value; }
        }
        [D2OIgnore]
        public Boolean Sex
        {
            get { return this.sex; }
            set { this.sex = value; }
        }
        [D2OIgnore]
        public Boolean IsRideable
        {
            get { return this.isRideable; }
            set { this.isRideable = value; }
        }
        [D2OIgnore]
        public Boolean IsFeconded
        {
            get { return this.isFeconded; }
            set { this.isFeconded = value; }
        }
        [D2OIgnore]
        public Boolean IsFecondationReady
        {
            get { return this.isFecondationReady; }
            set { this.isFecondationReady = value; }
        }
        [D2OIgnore]
        public int ReproductionCount
        {
            get { return this.reproductionCount; }
            set { this.reproductionCount = value; }
        }
        [D2OIgnore]
        public uint ReproductionCountMax
        {
            get { return this.reproductionCountMax; }
            set { this.reproductionCountMax = value; }
        }
        [D2OIgnore]
        public List<EffectInstanceInteger> Effects
        {
            get { return this.effects; }
            set { this.effects = value; }
        }
        [D2OIgnore]
        public List<uint> Capacities
        {
            get { return this.capacities; }
            set { this.capacities = value; }
        }
    }
}
