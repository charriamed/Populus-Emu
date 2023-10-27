using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("CharacterXPMapping", "com.ankamagames.dofus.datacenter.misc")]
    [Serializable]
    public class CharacterXPMapping : IDataObject
    {
        public const String MODULE = "CharacterXPMappings";
        public int level;
        public double experiencePoints;
        [D2OIgnore]
        public int Level
        {
            get { return this.level; }
            set { this.level = value; }
        }
        [D2OIgnore]
        public double ExperiencePoints
        {
            get { return this.experiencePoints; }
            set { this.experiencePoints = value; }
        }
    }
}
