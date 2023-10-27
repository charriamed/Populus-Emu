using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("MonsterDropCoefficient", "com.ankamagames.dofus.datacenter.monsters")]
    [Serializable]
    public class MonsterDropCoefficient : IDataObject, IIndexedData
    {
        public uint monsterId;
        public uint monsterGrade;
        public double dropCoefficient;
        public String criteria;
        int IIndexedData.Id
        {
            get { return (int)monsterId; }
        }
        [D2OIgnore]
        public uint MonsterId
        {
            get { return this.monsterId; }
            set { this.monsterId = value; }
        }
        [D2OIgnore]
        public uint MonsterGrade
        {
            get { return this.monsterGrade; }
            set { this.monsterGrade = value; }
        }
        [D2OIgnore]
        public double DropCoefficient
        {
            get { return this.dropCoefficient; }
            set { this.dropCoefficient = value; }
        }
        [D2OIgnore]
        public String Criteria
        {
            get { return this.criteria; }
            set { this.criteria = value; }
        }
    }
}
