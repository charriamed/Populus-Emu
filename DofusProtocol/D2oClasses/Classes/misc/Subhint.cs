using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("Subhint", "com.ankamagames.dofus.datacenter.misc")]
    [Serializable]
    public class Subhint : IDataObject, IIndexedData
    {
        public const String MODULE = "Subhints";
        public int hint_id;
        public String hint_parent_uid;
        public String hint_anchored_element;
        public int hint_anchor;
        public int hint_position_x;
        public int hint_position_y;
        public int hint_width;
        public int hint_height;
        public String hint_highlighted_element;
        public int hint_order;
        [I18NField]
        public uint hint_tooltip_text;
        public int hint_tooltip_position_enum;
        public String hint_tooltip_url;
        public int hint_tooltip_offset_x;
        public int hint_tooltip_offset_y;
        public int hint_tooltip_width;
        public double hint_creation_date;
        int IIndexedData.Id
        {
            get { return (int)hint_id; }
        }
        [D2OIgnore]
        public int Hint_id
        {
            get { return this.hint_id; }
            set { this.hint_id = value; }
        }
        [D2OIgnore]
        public String Hint_parent_uid
        {
            get { return this.hint_parent_uid; }
            set { this.hint_parent_uid = value; }
        }
        [D2OIgnore]
        public String Hint_anchored_element
        {
            get { return this.hint_anchored_element; }
            set { this.hint_anchored_element = value; }
        }
        [D2OIgnore]
        public int Hint_anchor
        {
            get { return this.hint_anchor; }
            set { this.hint_anchor = value; }
        }
        [D2OIgnore]
        public int Hint_position_x
        {
            get { return this.hint_position_x; }
            set { this.hint_position_x = value; }
        }
        [D2OIgnore]
        public int Hint_position_y
        {
            get { return this.hint_position_y; }
            set { this.hint_position_y = value; }
        }
        [D2OIgnore]
        public int Hint_width
        {
            get { return this.hint_width; }
            set { this.hint_width = value; }
        }
        [D2OIgnore]
        public int Hint_height
        {
            get { return this.hint_height; }
            set { this.hint_height = value; }
        }
        [D2OIgnore]
        public String Hint_highlighted_element
        {
            get { return this.hint_highlighted_element; }
            set { this.hint_highlighted_element = value; }
        }
        [D2OIgnore]
        public int Hint_order
        {
            get { return this.hint_order; }
            set { this.hint_order = value; }
        }
        [D2OIgnore]
        public uint Hint_tooltip_text
        {
            get { return this.hint_tooltip_text; }
            set { this.hint_tooltip_text = value; }
        }
        [D2OIgnore]
        public int Hint_tooltip_position_enum
        {
            get { return this.hint_tooltip_position_enum; }
            set { this.hint_tooltip_position_enum = value; }
        }
        [D2OIgnore]
        public String Hint_tooltip_url
        {
            get { return this.hint_tooltip_url; }
            set { this.hint_tooltip_url = value; }
        }
        [D2OIgnore]
        public int Hint_tooltip_offset_x
        {
            get { return this.hint_tooltip_offset_x; }
            set { this.hint_tooltip_offset_x = value; }
        }
        [D2OIgnore]
        public int Hint_tooltip_offset_y
        {
            get { return this.hint_tooltip_offset_y; }
            set { this.hint_tooltip_offset_y = value; }
        }
        [D2OIgnore]
        public int Hint_tooltip_width
        {
            get { return this.hint_tooltip_width; }
            set { this.hint_tooltip_width = value; }
        }
        [D2OIgnore]
        public double Hint_creation_date
        {
            get { return this.hint_creation_date; }
            set { this.hint_creation_date = value; }
        }
    }
}
