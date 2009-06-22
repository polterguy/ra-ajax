/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 * Authors: 
 * Thomas Hansen (thomas@ra-ajax.org)
 * Kariem Ali (kariem@ra-ajax.org)
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Ra.Builder;

namespace Ra.Widgets
{
    /**
     * SelectList control. This is the equivalent of the HTML select element. Also the equivalent of the 
     * ASP.NET DropDownList (and partially also) the ListBox control. This one only have support for single
     * selections.
     */
    [ASP.ParseChildren(true, "Items")]
    [ASP.ToolboxData("<{0}:SelectList runat=server />")]
    public class SelectList : RaWebControl
    {
        /**
         * Raised when selected index is changed
         */
        public event EventHandler SelectedIndexChanged;

        /**
         * Raised when button looses focus, opposite of Focused
         */
        public event EventHandler Blur;

        /**
         * Raised when button receives Focus, opposite of Blur
         */
        public event EventHandler Focused;

        private ListItemCollection _listItems;
        private string _selectedItemValue;

        public SelectList()
        {
            _listItems = new ListItemCollection(this);
        }

        #region [ -- Properties -- ]

        /**
         * The keyboard shortcut for giving the SelectList focus. Most browsers implements
         * some type of keyboard shortcut logic like for instance FireFox allows
         * form elements to be triggered by combining the AccessKey value (single character)
         * together with ALT and SHIFT. Meaning if you have e.g. "H" as keyboard shortcut
         * you can give the dropdownlist focus by clicking ALT+SHIFT+H on your 
         * keyboard. The combinations to effectuate the keyboard shortcuts however vary from 
         * browsers to browsers.
         */
        [DefaultValue("")]
        public string AccessKey
        {
            get { return ViewState["AccessKey"] == null ? "" : (string)ViewState["AccessKey"]; }
            set
            {
                if (value != AccessKey)
                    SetJSONValueString("AccessKey", value);
                ViewState["AccessKey"] = value;
            }
        }

        /**
         * If false then the checkbox is disabled, otherwise it is enabled
         */
        [DefaultValue(true)]
        public bool Enabled
        {
            get { return ViewState["Enabled"] == null ? true : (bool)ViewState["Enabled"]; }
            set
            {
                if (value != Enabled)
                    SetJSONGenericValue("disabled", (value ? "" : "disabled"));
                ViewState["Enabled"] = value;
            }
        }

        [DefaultValue(1)]
        public int Size
        {
            get { return ViewState["Size"] == null ? 1 : (int)ViewState["Size"]; }
            set
            {
                if (value != Size)
                    SetJSONGenericValue("size", value.ToString());
                ViewState["Size"] = value;
            }
        }

        /**
         * List of ListItems currently in the dropdownlist
         */
        [ASP.PersistenceMode(ASP.PersistenceMode.InnerDefaultProperty)]
        public ListItemCollection Items
        {
            get
            {
                return _listItems;
            }
        }

        /**
         * Currently selected item, active item from Items collection
         */
        public ListItem SelectedItem
        {
            get
            {
                if (_selectedItemValue == null)
                {
                    if (Items == null || Items.Count == 0)
                        return null;
                    return Items[0];
                }
                return Items.Find(
                    delegate(ListItem idx)
                    {
                        return idx.Value == _selectedItemValue;
                    });
            }
            set
            {
                _selectedItemValue = value.Value;
                if (IsTrackingViewState)
                {
                    this.SetJSONValueString("Value", value.Value);
                }
                SelectedIndex = Items.IndexOf(value);
            }
        }

        /**
         * Currently selected item, active item from Items collection
         */
        public int SelectedIndex
        {
            get
            {
                if (Items == null || Items.Count == 0)
                    return -1;
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i].Selected)
                        return i;
                }
                return 0;
            }
            set
            {
                if (value == SelectedIndex)
                    return;
                for (int i = 0; i < Items.Count; i++)
                {
                    if (i == value)
                        _selectedItemValue = Items[i].Value;
                }
                if (IsTrackingViewState)
                {
                    SetJSONGenericValue("value", _selectedItemValue);
                }
            }
        }

        #endregion

        protected override void TrackViewState()
        {
            Items.TrackViewState();
            base.TrackViewState();
        }

        private void SetSelectedItem()
        {
            string newVal = ((Page)HttpContext.Current.CurrentHandler).Request.Params[ClientID];
            if (newVal != null)
                _selectedItemValue = newVal;
        }

        protected override object SaveViewState()
        {
            object[] retVal = new object[2];
            retVal[0] = Items.SaveViewState();
            ViewState["_selectedItemValue"] = _selectedItemValue;
            retVal[1] = base.SaveViewState();
            return retVal;
        }

        protected override void LoadViewState(object savedState)
        {
            object[] content = savedState as object[];
            Items.LoadViewState(content[0]);
            base.LoadViewState(content[1]);
            if (_selectedItemValue == null)
                _selectedItemValue = (string)ViewState["_selectedItemValue"];
        }

        protected override void OnInit(EventArgs e)
        {
            // Since if ViewState is DISABLED we will NEVER come into LoadViewState we need to
            // have the same logic in OnInit since we really should modify the Text value to
            // the postback value BEFORE Page_Load event is fired...
            if (Enabled && ((Page)HttpContext.Current.CurrentHandler).IsPostBack)
            {
                SetSelectedItem();
            }
            base.OnInit(e);
        }

        #region [ -- Overridden (abstract/virtual) methods from RaControl -- ]

        // Override this one to handle events fired on the client-side
        public override void DispatchEvent(string name)
        {
            switch (name)
            {
                case "change":
                    if (SelectedIndexChanged != null)
                        SelectedIndexChanged(this, new EventArgs());
                    break;
                case "blur":
                    if (Blur != null)
                        Blur(this, new EventArgs());
                    break;
                case "focus":
                    if (Focused != null)
                        Focused(this, new EventArgs());
                    break;
                default:
                    base.DispatchEvent(name);
                    break;
            }
        }

        protected override string GetEventsRegisterScript()
        {
            string evts = base.GetEventsRegisterScript();

            if (SelectedIndexChanged != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['change']";
            }
            if (Blur != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['blur']";
            }
            if (Focused != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['focus']";
            }
			return evts;
        }

        protected override void RenderRaControl(HtmlBuilder builder)
        {
            using (Element el = builder.CreateElement("select"))
            {
                AddAttributes(el);
                foreach (ListItem idx in Items)
                {
                    using (Element l = builder.CreateElement("option"))
                    {
                        l.AddAttribute("value", idx.Value);
                        if (!idx.Enabled)
                            l.AddAttribute("disabled", "disabled");
                        if (idx.Selected)
                            l.AddAttribute("selected", "selected");
                        l.Write(idx.Text);
                    }
                }
            }
        }

        protected override void AddAttributes(Element el)
        {
            el.AddAttribute("name", ClientID);
            if (!string.IsNullOrEmpty(AccessKey))
                el.AddAttribute("accesskey", AccessKey);
            if (Size != -1)
                el.AddAttribute("size", Size.ToString());
            if (!Enabled)
                el.AddAttribute("disabled", "disabled");
            base.AddAttributes(el);
        }

        #endregion
    }
}
































