using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace WorldEditor.D2OClasses
{    
    [Serializable]
    [D2OClass("AnimFunNpcData", "com.ankamagames.dofus.datacenter.npcs")]
    public class AnimFunNpcData : AnimFunData
    {
        public List<AnimFunNpcData> subAnimFunData;
    }
}