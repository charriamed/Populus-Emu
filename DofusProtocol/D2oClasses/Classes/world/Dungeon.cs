﻿using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("Dungeon", "com.ankamagames.dofus.datacenter.world")]
    [Serializable]
    public class Dungeon : IDataObject, IIndexedData
    {
        public const String MODULE = "Dungeons";
        public int id;
        [I18NField]
        public uint nameId;
        public int optimalPlayerLevel;
        public List<double> mapIds;
        public double entranceMapId;
        public double exitMapId;
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
        public uint NameId
        {
            get { return this.nameId; }
            set { this.nameId = value; }
        }
        [D2OIgnore]
        public int OptimalPlayerLevel
        {
            get { return this.optimalPlayerLevel; }
            set { this.optimalPlayerLevel = value; }
        }
        [D2OIgnore]
        public List<double> MapIds
        {
            get { return this.mapIds; }
            set { this.mapIds = value; }
        }
        [D2OIgnore]
        public double EntranceMapId
        {
            get { return this.entranceMapId; }
            set { this.entranceMapId = value; }
        }
        [D2OIgnore]
        public double ExitMapId
        {
            get { return this.exitMapId; }
            set { this.exitMapId = value; }
        }
    }
}
