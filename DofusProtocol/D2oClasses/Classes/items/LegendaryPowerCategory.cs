using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("LegendaryPowerCategory", "com.ankamagames.dofus.datacenter.items")]
    [Serializable]
    public class LegendaryPowerCategory : IDataObject, IIndexedData
    {
        public const String MODULE = "LegendaryPowersCategories";
        public int id;
        public String categoryName;
        public Boolean categoryOverridable;
        public List<int> categorySpells;
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
        public String CategoryName
        {
            get { return this.categoryName; }
            set { this.categoryName = value; }
        }
        [D2OIgnore]
        public Boolean CategoryOverridable
        {
            get { return this.categoryOverridable; }
            set { this.categoryOverridable = value; }
        }
        [D2OIgnore]
        public List<int> CategorySpells
        {
            get { return this.categorySpells; }
            set { this.categorySpells = value; }
        }
    }
}
