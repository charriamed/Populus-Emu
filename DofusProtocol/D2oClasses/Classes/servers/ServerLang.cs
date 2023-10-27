using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("ServerLang", "com.ankamagames.dofus.datacenter.servers")]
    [Serializable]
    public class ServerLang : IDataObject, IIndexedData
    {
        public const String MODULE = "ServerLangs";
        public int id;
        [I18NField]
        public uint nameId;
        public String langCode;
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
        public String LangCode
        {
            get { return this.langCode; }
            set { this.langCode = value; }
        }
    }
}
