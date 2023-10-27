using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("MapCoordinates", "com.ankamagames.dofus.datacenter.world")]
    [Serializable]
    public class MapCoordinates : IDataObject
    {
        public const String MODULE = "MapCoordinates";
        public uint compressedCoords;
        public List<double> mapIds;
        [D2OIgnore]
        public uint CompressedCoords
        {
            get { return this.compressedCoords; }
            set { this.compressedCoords = value; }
        }
        [D2OIgnore]
        public List<double> MapIds
        {
            get { return this.mapIds; }
            set { this.mapIds = value; }
        }
    }
}
