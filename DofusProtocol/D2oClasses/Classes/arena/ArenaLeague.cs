using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("ArenaLeague", "com.ankamagames.dofus.datacenter.arena")]
    [Serializable]
    public class ArenaLeague : IDataObject, IIndexedData
    {
        public const String MODULE = "ArenaLeagues";
        public int id;
        [I18NField]
        public uint nameId;
        public uint ornamentId;
        public String icon;
        public String illus;
        public Boolean isLastLeague;
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
        public uint OrnamentId
        {
            get { return this.ornamentId; }
            set { this.ornamentId = value; }
        }
        [D2OIgnore]
        public String Icon
        {
            get { return this.icon; }
            set { this.icon = value; }
        }
        [D2OIgnore]
        public String Illus
        {
            get { return this.illus; }
            set { this.illus = value; }
        }
        [D2OIgnore]
        public Boolean IsLastLeague
        {
            get { return this.isLastLeague; }
            set { this.isLastLeague = value; }
        }
    }
}
