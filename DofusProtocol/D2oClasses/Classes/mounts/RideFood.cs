﻿using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("RideFood", "com.ankamagames.dofus.datacenter.mounts")]
    [Serializable]
    public class RideFood : IDataObject
    {
        public String MODULE = "RideFood";
        public uint gid;
        public uint typeId;
        public uint familyId;
        [D2OIgnore]
        public uint Gid
        {
            get { return this.gid; }
            set { this.gid = value; }
        }
        [D2OIgnore]
        public uint TypeId
        {
            get { return this.typeId; }
            set { this.typeId = value; }
        }
        [D2OIgnore]
        public uint FamilyId
        {
            get { return this.familyId; }
            set { this.familyId = value; }
        }
    }
}
