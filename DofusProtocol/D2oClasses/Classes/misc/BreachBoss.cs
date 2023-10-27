using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("BreachBoss", "com.ankamagames.dofus.datacenter.misc")]
    [Serializable]
    public class BreachBoss : IDataObject, IIndexedData
    {
        public const String MODULE = "BreachBosses";
        public int id;
        public int monsterId;
        public int category;
        public String apparitionCriterion;
        public String accessCriterion;
        public int maxRewardQuantity;
        public List<int> incompatibleBosses;
        public uint rewardId;
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
        public int MonsterId
        {
            get { return this.monsterId; }
            set { this.monsterId = value; }
        }
        [D2OIgnore]
        public int Category
        {
            get { return this.category; }
            set { this.category = value; }
        }
        [D2OIgnore]
        public String ApparitionCriterion
        {
            get { return this.apparitionCriterion; }
            set { this.apparitionCriterion = value; }
        }
        [D2OIgnore]
        public String AccessCriterion
        {
            get { return this.accessCriterion; }
            set { this.accessCriterion = value; }
        }
        [D2OIgnore]
        public int MaxRewardQuantity
        {
            get { return this.maxRewardQuantity; }
            set { this.maxRewardQuantity = value; }
        }
        [D2OIgnore]
        public List<int> IncompatibleBosses
        {
            get { return this.incompatibleBosses; }
            set { this.incompatibleBosses = value; }
        }
        [D2OIgnore]
        public uint RewardId
        {
            get { return this.rewardId; }
            set { this.rewardId = value; }
        }
    }
}
