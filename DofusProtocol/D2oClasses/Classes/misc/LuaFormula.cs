using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("LuaFormula", "com.ankamagames.dofus.datacenter.misc")]
    [Serializable]
    public class LuaFormula : IDataObject, IIndexedData
    {
        public const String MODULE = "LuaFormulas";
        public int id;
        public String formulaName;
        public String luaFormula;
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
        public String FormulaName
        {
            get { return this.formulaName; }
            set { this.formulaName = value; }
        }
        [D2OIgnore]
        public String LuaFormula_
        {
            get { return this.luaFormula; }
            set { this.luaFormula = value; }
        }
    }
}
