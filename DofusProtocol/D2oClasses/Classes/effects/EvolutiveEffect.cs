using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("EvolutiveEffect", "com.ankamagames.dofus.datacenter.effects")]
    [Serializable]
    public class EvolutiveEffect : IDataObject, IIndexedData
    {
        public const String MODULE = "EvolutiveEffects";
        public int id;
        public int actionId;
        public int targetId;
        public List<List<double>> progressionPerLevelRange;
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
        public int ActionId
        {
            get { return this.actionId; }
            set { this.actionId = value; }
        }
        [D2OIgnore]
        public int TargetId
        {
            get { return this.targetId; }
            set { this.targetId = value; }
        }
        [D2OIgnore]
        public List<List<double>> ProgressionPerLevelRange
        {
            get { return this.progressionPerLevelRange; }
            set { this.progressionPerLevelRange = value; }
        }
    }
}
