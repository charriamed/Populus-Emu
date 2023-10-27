using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("MonsterRace", "com.ankamagames.dofus.datacenter.monsters")]
    [Serializable]
    public class MonsterRace : IDataObject, IIndexedData
    {
        public const String MODULE = "MonsterRaces";
        public int id;
        public int superRaceId;
        [I18NField]
        public uint nameId;
        public List<uint> monsters;
        public int aggressiveZoneSize;
        public int aggressiveLevelDiff;
        public String aggressiveImmunityCriterion;
        public int aggressiveAttackDelay;
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
        public int SuperRaceId
        {
            get { return this.superRaceId; }
            set { this.superRaceId = value; }
        }
        [D2OIgnore]
        public uint NameId
        {
            get { return this.nameId; }
            set { this.nameId = value; }
        }
        [D2OIgnore]
        public List<uint> Monsters
        {
            get { return this.monsters; }
            set { this.monsters = value; }
        }
        [D2OIgnore]
        public int AggressiveZoneSize
        {
            get { return this.aggressiveZoneSize; }
            set { this.aggressiveZoneSize = value; }
        }
        [D2OIgnore]
        public int AggressiveLevelDiff
        {
            get { return this.aggressiveLevelDiff; }
            set { this.aggressiveLevelDiff = value; }
        }
        [D2OIgnore]
        public String AggressiveImmunityCriterion
        {
            get { return this.aggressiveImmunityCriterion; }
            set { this.aggressiveImmunityCriterion = value; }
        }
        [D2OIgnore]
        public int AggressiveAttackDelay
        {
            get { return this.aggressiveAttackDelay; }
            set { this.aggressiveAttackDelay = value; }
        }
    }
}
