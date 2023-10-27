using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("SpellVariant", "com.ankamagames.dofus.datacenter.spells")]
    [Serializable]
    public class SpellVariant : IDataObject, IIndexedData
    {
        public const String MODULE = "SpellVariants";
        public int id;
        public uint breedId;
        public List<uint> spellIds;
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
        public uint BreedId
        {
            get { return this.breedId; }
            set { this.breedId = value; }
        }
        [D2OIgnore]
        public List<uint> SpellIds
        {
            get { return this.spellIds; }
            set { this.spellIds = value; }
        }
    }
}
