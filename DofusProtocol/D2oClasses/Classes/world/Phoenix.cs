using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("Phoenix", "com.ankamagames.dofus.datacenter.world")]
    [Serializable]
    public class Phoenix : IDataObject
    {
        public const String MODULE = "Phoenixes";
        public double mapId;
        [D2OIgnore]
        public double MapId
        {
            get { return this.mapId; }
            set { this.mapId = value; }
        }
    }
}
