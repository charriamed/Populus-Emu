using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("ItemType", "com.ankamagames.dofus.datacenter.items")]
    [Serializable]
    public class ItemType : IDataObject, IIndexedData
    {
        public const String MODULE = "ItemTypes";
        public int id;
        [I18NField]
        public uint nameId;
        public uint superTypeId;
        public uint categoryId;
        public Boolean isInEncyclopedia;
        public Boolean plural;
        public uint gender;
        public String rawZone;
        public Boolean mimickable;
        public int craftXpRatio;
        public int evolutiveTypeId;
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
        public uint SuperTypeId
        {
            get { return this.superTypeId; }
            set { this.superTypeId = value; }
        }
        [D2OIgnore]
        public uint CategoryId
        {
            get { return this.categoryId; }
            set { this.categoryId = value; }
        }
        [D2OIgnore]
        public Boolean IsInEncyclopedia
        {
            get { return this.isInEncyclopedia; }
            set { this.isInEncyclopedia = value; }
        }
        [D2OIgnore]
        public Boolean Plural
        {
            get { return this.plural; }
            set { this.plural = value; }
        }
        [D2OIgnore]
        public uint Gender
        {
            get { return this.gender; }
            set { this.gender = value; }
        }
        [D2OIgnore]
        public String RawZone
        {
            get { return this.rawZone; }
            set { this.rawZone = value; }
        }
        [D2OIgnore]
        public Boolean Mimickable
        {
            get { return this.mimickable; }
            set { this.mimickable = value; }
        }
        [D2OIgnore]
        public int CraftXpRatio
        {
            get { return this.craftXpRatio; }
            set { this.craftXpRatio = value; }
        }
        [D2OIgnore]
        public int EvolutiveTypeId
        {
            get { return this.evolutiveTypeId; }
            set { this.evolutiveTypeId = value; }
        }
    }
}
