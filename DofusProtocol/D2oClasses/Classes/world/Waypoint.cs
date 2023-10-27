using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("Waypoint", "com.ankamagames.dofus.datacenter.world")]
    [Serializable]
    public class Waypoint : IDataObject, IIndexedData
    {
        public const String MODULE = "Waypoints";
        public uint id;
        public double mapId;
        public uint subAreaId;
        public Boolean activated;
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
        public double MapId
        {
            get { return this.mapId; }
            set { this.mapId = value; }
        }
        [D2OIgnore]
        public uint SubAreaId
        {
            get { return this.subAreaId; }
            set { this.subAreaId = value; }
        }
        [D2OIgnore]
        public Boolean Activated
        {
            get { return this.activated; }
            set { this.activated = value; }
        }
    }
}
