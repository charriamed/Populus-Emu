﻿using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("MonsterXPBonus", "com.ankamagames.dofus.datacenter.bonus")]
    [Serializable]
    public class MonsterXPBonus : MonsterBonus
    {
    }
}
