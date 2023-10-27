using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("Sign", "com.ankamagames.dofus.datacenter.interactives")]
    [Serializable]
    public class Sign : IDataObject, IIndexedData
    {
        public const String MODULE = "Signs";
        public int id;
        public String @params;
        public int skillId;
        [I18NField]
        public uint textKey;
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
        public String Params
        {
            get { return this.@params; }
            set { this.@params = value; }
        }
        [D2OIgnore]
        public int SkillId
        {
            get { return this.skillId; }
            set { this.skillId = value; }
        }
        [D2OIgnore]
        public uint TextKey
        {
            get { return this.textKey; }
            set { this.textKey = value; }
        }
    }
}
