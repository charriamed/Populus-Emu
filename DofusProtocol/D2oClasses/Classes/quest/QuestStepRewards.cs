using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("QuestStepRewards", "com.ankamagames.dofus.datacenter.quest")]
    [Serializable]
    public class QuestStepRewards : IDataObject, IIndexedData
    {
        public const String MODULE = "QuestStepRewards";
        public uint id;
        public uint stepId;
        public int levelMin;
        public int levelMax;
        public double kamasRatio;
        public double experienceRatio;
        public Boolean kamasScaleWithPlayerLevel;
        public List<List<uint>> itemsReward;
        public List<uint> emotesReward;
        public List<uint> jobsReward;
        public List<uint> spellsReward;
        public List<uint> titlesReward;
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
        public uint StepId
        {
            get { return this.stepId; }
            set { this.stepId = value; }
        }
        [D2OIgnore]
        public int LevelMin
        {
            get { return this.levelMin; }
            set { this.levelMin = value; }
        }
        [D2OIgnore]
        public int LevelMax
        {
            get { return this.levelMax; }
            set { this.levelMax = value; }
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
        public List<List<uint>> ItemsReward
        {
            get { return this.itemsReward; }
            set { this.itemsReward = value; }
        }
        [D2OIgnore]
        public List<uint> EmotesReward
        {
            get { return this.emotesReward; }
            set { this.emotesReward = value; }
        }
        [D2OIgnore]
        public List<uint> JobsReward
        {
            get { return this.jobsReward; }
            set { this.jobsReward = value; }
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
    }
}
