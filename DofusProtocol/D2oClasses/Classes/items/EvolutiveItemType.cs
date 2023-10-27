using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("EvolutiveItemType", "com.ankamagames.dofus.datacenter.items")]
    [Serializable]
    public class EvolutiveItemType : IDataObject, IIndexedData
    {
        public const String MODULE = "EvolutiveItemTypes";
        public int id;
        public uint maxLevel;
        public double experienceBoost;
        public List<int> experienceByLevel;
        int IIndexedData.Id
        {
            get { return (int)id; }
        }
        [D2OIgnore]
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        [D2OIgnore]
        public uint MaxLevel
        {
            get { return this.maxLevel; }
            set { this.maxLevel = value; }
        }
        [D2OIgnore]
        public double ExperienceBoost
        {
            get { return this.experienceBoost; }
            set { this.experienceBoost = value; }
        }
        [D2OIgnore]
        public List<int> ExperienceByLevel
        {
            get { return this.experienceByLevel; }
            set { this.experienceByLevel = value; }
        }
    }
}
