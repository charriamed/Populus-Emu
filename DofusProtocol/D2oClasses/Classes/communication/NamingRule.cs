using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("NamingRule", "com.ankamagames.dofus.datacenter.communication")]
    [Serializable]
    public class NamingRule : IDataObject, IIndexedData
    {
        public const String MODULE = "NamingRules";
        public uint id;
        public uint minLength;
        public uint maxLength;
        public String regexp;
        int IIndexedData.Id
        {
            get { return (int)id; }
        }
        [D2OIgnore]
        public uint Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        [D2OIgnore]
        public uint MinLength
        {
            get { return this.minLength; }
            set { this.minLength = value; }
        }
        [D2OIgnore]
        public uint MaxLength
        {
            get { return this.maxLength; }
            set { this.maxLength = value; }
        }
        [D2OIgnore]
        public String Regexp
        {
            get { return this.regexp; }
            set { this.regexp = value; }
        }
    }
}
