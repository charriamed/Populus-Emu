using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("BreachPrize", "com.ankamagames.dofus.datacenter.misc")]
    [Serializable]
    public class BreachPrize : IDataObject, IIndexedData
    {
        public const String MODULE = "BreachPrizes";
        public int id;
        [I18NField]
        public uint nameId;
        public int currency;
        public String tooltipKey;
        [I18NField]
        public uint descriptionKey;
        public int categoryId;
        public int itemId;
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
        public int Currency
        {
            get { return this.currency; }
            set { this.currency = value; }
        }
        [D2OIgnore]
        public String TooltipKey
        {
            get { return this.tooltipKey; }
            set { this.tooltipKey = value; }
        }
        [D2OIgnore]
        public uint DescriptionKey
        {
            get { return this.descriptionKey; }
            set { this.descriptionKey = value; }
        }
        [D2OIgnore]
        public int CategoryId
        {
            get { return this.categoryId; }
            set { this.categoryId = value; }
        }
        [D2OIgnore]
        public int ItemId
        {
            get { return this.itemId; }
            set { this.itemId = value; }
        }
    }
}
