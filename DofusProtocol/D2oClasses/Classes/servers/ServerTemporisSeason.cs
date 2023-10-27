using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("ServerTemporisSeason", "com.ankamagames.dofus.datacenter.servers")]
    [Serializable]
    public class ServerTemporisSeason : IDataObject, IIndexedData
    {
        public const String MODULE = "ServerTemporisSeasons";
        public int uid;
        public uint seasonNumber;
        public String information;
        public double beginning;
        public double closure;
        int IIndexedData.Id
        {
            get { return (int)uid; }
        }
        [D2OIgnore]
        public int Uid
        {
            get { return this.uid; }
            set { this.uid = value; }
        }
        [D2OIgnore]
        public uint SeasonNumber
        {
            get { return this.seasonNumber; }
            set { this.seasonNumber = value; }
        }
        [D2OIgnore]
        public String Information
        {
            get { return this.information; }
            set { this.information = value; }
        }
        [D2OIgnore]
        public double Beginning
        {
            get { return this.beginning; }
            set { this.beginning = value; }
        }
        [D2OIgnore]
        public double Closure
        {
            get { return this.closure; }
            set { this.closure = value; }
        }
    }
}
