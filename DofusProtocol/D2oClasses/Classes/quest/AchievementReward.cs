using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("AchievementReward", "com.ankamagames.dofus.datacenter.quest")]
    [Serializable]
    public class AchievementReward : IDataObject, IIndexedData
    {
        public const String MODULE = "AchievementRewards";
        public uint id;
        public uint achievementId;
        public String criteria;
        public double kamasRatio;
        public double experienceRatio;
        public Boolean kamasScaleWithPlayerLevel;
        public List<uint> itemsReward;
        public List<uint> itemsQuantityReward;
        public List<uint> emotesReward;
        public List<uint> spellsReward;
        public List<uint> titlesReward;
        public List<uint> ornamentsReward;
        int IIndexedData.Id
        {
            get { return (int)id; }
        }
        [D2OIgnore]
        public uint Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        [D2OIgnore]
        public uint AchievementId
        {
            get { return this.achievementId; }
            set { this.achievementId = value; }
        }
        [D2OIgnore]
        public String Criteria
        {
            get { return this.criteria; }
            set { this.criteria = value; }
        }
        [D2OIgnore]
        public double KamasRatio
        {
            get { return this.kamasRatio; }
            set { this.kamasRatio = value; }
        }
        [D2OIgnore]
        public double ExperienceRatio
        {
            get { return this.experienceRatio; }
            set { this.experienceRatio = value; }
        }
        [D2OIgnore]
        public Boolean KamasScaleWithPlayerLevel
        {
            get { return this.kamasScaleWithPlayerLevel; }
            set { this.kamasScaleWithPlayerLevel = value; }
        }
        [D2OIgnore]
        public List<uint> ItemsReward
        {
            get { return this.itemsReward; }
            set { this.itemsReward = value; }
        }
        [D2OIgnore]
        public List<uint> ItemsQuantityReward
        {
            get { return this.itemsQuantityReward; }
            set { this.itemsQuantityReward = value; }
        }
        [D2OIgnore]
        public List<uint> EmotesReward
        {
            get { return this.emotesReward; }
            set { this.emotesReward = value; }
        }
        [D2OIgnore]
        public List<uint> SpellsReward
        {
            get { return this.spellsReward; }
            set { this.spellsReward = value; }
        }
        [D2OIgnore]
        public List<uint> TitlesReward
        {
            get { return this.titlesReward; }
            set { this.titlesReward = value; }
        }
        [D2OIgnore]
        public List<uint> OrnamentsReward
        {
            get { return this.ornamentsReward; }
            set { this.ornamentsReward = value; }
        }
    }
}
