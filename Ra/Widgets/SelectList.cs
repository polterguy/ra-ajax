/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using System.Collections.Generic;

namespace Ra.Widgets
{
    /**
     * SelectList control, renders &lt;select...
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

        [DefaultValue(-1)]
        public int Size
        {
            get { return ViewState["Size"] == null ? -1 : (int)ViewState["Size"]; }
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
                    return Items[0];
                return Items.Find(
                    delegate(ListItem idx)
                    {
                        return idx.Value == _selectedItemValue;
                    });
            }
            set
            {
                if (IsTrackingViewState)
                {
                    foreach (ListItem idx in Items)
                    {
                        if (idx != value)
                            idx.Selected = false;
                    }
                    _selectedItemValue = value.Value;
                    this.SetJSONValueString("Value", value.Value);
                }
            }
        }

        #endregion

        protected override void TrackViewState()
        {
            Items.TrackViewState();
            base.TrackViewState();
        }

        protected override void LoadViewState(object savedState)
        {
            object[] content = savedState as object[];
            Items.LoadViewState(content[0]);
            base.LoadViewState(content[1]);

            // Since if ViewState is DISABLED we will NEVER come into this bugger we need to
            // have the same logic in OnInit since we really should modify the Text value to
            // the postback value BEFORE Page_Load event is fired...
            if (Enabled && AjaxManager.Instance.CurrentPage.IsPostBack)
            {
                _selectedItemValue = AjaxManager.Instance.CurrentPage.Request.Params[ClientID];
            }
        }

        protected override object SaveViewState()
        {
            object[] retVal = new object[2];
            retVal[0] = Items.SaveViewState();
            retVal[1] = base.SaveViewState();
            return retVal;
        }

        protected override void OnInit(EventArgs e)
        {
            // Since if ViewState is DISABLED we will NEVER come into LoadViewState we need to
            // have the same logic in OnInit since we really should modify the Text value to
            // the postback value BEFORE Page_Load event is fired...
            if (Enabled && !this.IsViewStateEnabled && AjaxManager.Instance.CurrentPage.IsPostBack)
            {
                _selectedItemValue = AjaxManager.Instance.CurrentPage.Request.Params[ClientID];
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

        // Override this one to create specific HTML for your widgets
        protected override string GetOpeningHTML()
        {
            string accessKey = string.IsNullOrEmpty(AccessKey) ? "" : string.Format(" accesskey=\"{0}\"", AccessKey);
            string sizeString = Size == -1 ? "" : (" size=\"" + Size + "\"");
            return string.Format("<select{6} name=\"{0}\" id=\"{0}\"{1}{2}{3}{5}>{4}</select>",
                ClientID,
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute(),
                accessKey,
                GetHTMLForOptions(),
                (Enabled ? "" : " disabled=\"disabled\""),
                sizeString);
        }

        private string GetHTMLForOptions()
        {
            string retVal = "";
            foreach (ListItem idx in Items)
            {
                bool isSelected = false;
                if (_selectedItemValue != null)
                    isSelected = _selectedItemValue == idx.Value;
                else
                    isSelected = idx.Selected;
                retVal += string.Format("<option value=\"{0}\"{2}{3}>{1}</option>",
                    idx.Value,
                    (string.IsNullOrEmpty(idx.Text) ? idx.Value : idx.Text),
                    (idx.Enabled ? "" : " disabled=\"disabled\""),
                    (isSelected ? " selected=\"selected\"" : ""));
            }
            return retVal;
        }

        #endregion
    }
}
































