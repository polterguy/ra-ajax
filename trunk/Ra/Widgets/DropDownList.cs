/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Helpers;
using System.Collections.Generic;

namespace Ra.Widgets
{
    [ASP.ParseChildren(true, "Items")]
    [ASP.ToolboxData("<{0}:DropDownList runat=server />")]
    public class DropDownList : RaWebControl, IRaControl
    {
        public event EventHandler SelectedIndexChanged;

        public event EventHandler Blur;

        public event EventHandler Focused;

        public event EventHandler MouseOver;

        public event EventHandler MouseOut;

        private ListItemCollection _listItems;

        public DropDownList()
        {
            _listItems = new ListItemCollection(this);
        }

        #region [ -- Properties -- ]

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

        [ASP.PersistenceMode(ASP.PersistenceMode.InnerDefaultProperty)]
        public ListItemCollection Items
        {
            get
            {
                return _listItems;
            }
        }

        private string _selectedItemValue;
        public ListItem SelectedItem
        {
            get
            {
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
                case "mouseover":
                    if (MouseOver != null)
                        MouseOver(this, new EventArgs());
                    break;
                case "mouseout":
                    if (MouseOut != null)
                        MouseOut(this, new EventArgs());
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
                    throw new ApplicationException("Unknown event fired for control");
            }
        }

        // Override this one to create specific initialization script for your widgets
        private bool _scriptRetrieved;
        public override string GetClientSideScript()
        {
            if (_scriptRetrieved)
                return "";
            _scriptRetrieved = true;
            string evts = "";
            if (SelectedIndexChanged != null)
                evts += "['change']";
            if (MouseOver != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['mouseover']";
            }
            if (MouseOut != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['mouseout']";
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
            if (evts.Length == 0)
            {
                // No events
                return string.Format("\r\nRa.C('{0}');", ClientID);
            }
            else
            {
                return string.Format("\r\nRa.C('{0}', {{evts:[{1}]}});", ClientID, evts);
            }
        }

        // Override this one to create specific HTML for your widgets
        public override string GetHTML()
        {
            string accessKey = string.IsNullOrEmpty(AccessKey) ? "" : string.Format(" accesskey=\"{0}\"", AccessKey);
            return string.Format("<select name=\"{0}\" id=\"{0}\"{1}{2}{3}{5}>{4}</select>",
                ClientID,
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute(),
                accessKey,
                GetHTMLForOptions(),
                (Enabled ? "" : "disabled=\"disabled\""));
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
                    (idx.Enabled ? "" : "disabled=\"disabled\""),
                    (isSelected ? "selected=\"selected\"" : ""));
            }
            return retVal;
        }

        #endregion
    }
}
































