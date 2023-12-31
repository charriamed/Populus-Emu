﻿using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("CensoredContent", "com.ankamagames.dofus.datacenter.misc")]
    [Serializable]
    public class CensoredContent : IDataObject
    {
        public const String MODULE = "CensoredContents";
        public int type;
        public int oldValue;
        public int newValue;
        public String lang;
        [D2OIgnore]
        public int Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        [D2OIgnore]
        public int OldValue
        {
            get { return this.oldValue; }
            set { this.oldValue = value; }
        }
        [D2OIgnore]
        public int NewValue
        {
            get { return this.newValue; }
            set { this.newValue = value; }
        }
        [D2OIgnore]
        public String Lang
        {
            get { return this.lang; }
            set { this.lang = value; }
        }
    }
}
