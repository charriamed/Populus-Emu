using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("Spell", "com.ankamagames.dofus.datacenter.spells")]
    [Serializable]
    public class Spell : IDataObject, IIndexedData
    {
        public const String MODULE = "Spells";
        public int id;
        public uint nameId;
        public uint descriptionId;
        public uint typeId;
        public uint order;
        public String scriptParams;
        public String scriptParamsCritical;
        public int scriptId;
        public int scriptIdCritical;
        public int iconId;
        public List<uint> spellLevels;
        public Boolean useParamCache = true;
        public Boolean verbose_cast;
        public String default_zone;
        public Boolean bypassSummoningLimit;
        public Boolean canAlwaysTriggerSpells;
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
        public uint DescriptionId
        {
            get { return this.descriptionId; }
            set { this.descriptionId = value; }
        }
        [D2OIgnore]
        public uint TypeId
        {
            get { return this.typeId; }
            set { this.typeId = value; }
        }
        [D2OIgnore]
        public uint Order
        {
            get { return this.order; }
            set { this.order = value; }
        }
        [D2OIgnore]
        public String ScriptParams
        {
            get { return this.scriptParams; }
            set { this.scriptParams = value; }
        }
        [D2OIgnore]
        public String ScriptParamsCritical
        {
            get { return this.scriptParamsCritical; }
            set { this.scriptParamsCritical = value; }
        }
        [D2OIgnore]
        public int ScriptId
        {
            get { return this.scriptId; }
            set { this.scriptId = value; }
        }
        [D2OIgnore]
        public int ScriptIdCritical
        {
            get { return this.scriptIdCritical; }
            set { this.scriptIdCritical = value; }
        }
        [D2OIgnore]
        public int IconId
        {
            get { return this.iconId; }
            set { this.iconId = value; }
        }
        [D2OIgnore]
        public List<uint> SpellLevels
        {
            get { return this.spellLevels; }
            set { this.spellLevels = value; }
        }
        [D2OIgnore]
        public Boolean UseParamCache
        {
            get { return this.useParamCache; }
            set { this.useParamCache = value; }
        }
        [D2OIgnore]
        public Boolean Verbose_cast
        {
            get { return this.verbose_cast; }
            set { this.verbose_cast = value; }
        }
        [D2OIgnore]
        public String Default_zone
        {
            get { return this.default_zone; }
            set { this.default_zone = value; }
        }
        [D2OIgnore]
        public Boolean BypassSummoningLimit
        {
            get { return this.bypassSummoningLimit; }
            set { this.bypassSummoningLimit = value; }
        }
        [D2OIgnore]
        public Boolean CanAlwaysTriggerSpells
        {
            get { return this.canAlwaysTriggerSpells; }
            set { this.canAlwaysTriggerSpells = value; }
        }
    }
}
