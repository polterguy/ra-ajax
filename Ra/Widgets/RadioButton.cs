/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Helpers;

namespace Ra.Widgets
{
    [DefaultProperty("Text")]
    [ASP.ToolboxData("<{0}:RadioButton runat=server />")]
    public class RadioButton : RaWebControl, IRaControl
    {
        public event EventHandler CheckedChanged;

        public event EventHandler Blur;

        public event EventHandler Focused;

        public event EventHandler MouseOver;

        public event EventHandler MouseOut;

        #region [ -- Properties -- ]

        [DefaultValue("")]
        public string Text
        {
            get { return ViewState["Text"] == null ? "" : (string)ViewState["Text"]; }
            set
            {
                if (value != Text)
                    SetJSONValueString("Text", value);
                ViewState["Text"] = value;
            }
        }

        [DefaultValue("")]
        public string GroupName
        {
            get { return ViewState["GroupName"] == null ? "" : (string)ViewState["GroupName"]; }
            set
            {
                if (value != GroupName)
                    SetJSONGenericValue("name", value);
                ViewState["GroupName"] = value;
            }
        }

        [DefaultValue(false)]
        public bool Checked
        {
            get { return ViewState["Checked"] == null ? false : (bool)ViewState["Checked"]; }
            set
            {
                if (value != Checked)
                    SetJSONGenericValue("checked", value ? "checked" : "");
                ViewState["Checked"] = value;
            }
        }

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

        #endregion

        protected override void OnInit(EventArgs e)
        {
            // Since if ViewState is DISABLED we will NEVER come into LoadViewState we need to
            // have the same logic in OnInit since we really should modify the Text value to
            // the postback value BEFORE Page_Load event is fired...
            if (Enabled && !this.IsViewStateEnabled && AjaxManager.Instance.CurrentPage.IsPostBack)
            {
                bool valueOfChecked = 
                    AjaxManager.Instance.CurrentPage.Request.Params[
                        string.IsNullOrEmpty(GroupName) ? ClientID : GroupName] == ClientID;
                if (valueOfChecked != Checked)
                    Checked = valueOfChecked;
            }
            base.OnInit(e);
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            // Since if ViewState is DISABLED we will NEVER come into this bugger we need to
            // have the same logic in OnInit since we really should modify the Text value to
            // the postback value BEFORE Page_Load event is fired...
            if (Enabled && AjaxManager.Instance.CurrentPage.IsPostBack)
            {
                bool valueOfChecked =
                    AjaxManager.Instance.CurrentPage.Request.Params[
                        string.IsNullOrEmpty(GroupName) ? ClientID : GroupName] == ClientID;
                if (valueOfChecked != Checked)
                    Checked = valueOfChecked;
            }
        }

        #region [ -- Overridden (abstract/virtual) methods from RaControl -- ]

        // Override this one to handle events fired on the client-side
        public override void DispatchEvent(string name)
        {
            switch (name)
            {
                case "change":
                    if (CheckedChanged != null)
                        CheckedChanged(this, new EventArgs());
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
            if (CheckedChanged != null)
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
                if (_hasSetFocus)
                {
                    string options = "";
                    if (_hasSetFocus)
                        options += ",focus:true";
                    return string.Format("\r\nRa.C('{0}',{{ctrl:'{0}_CTRL',label:'{0}_LBL'{1}}});", ClientID, options);
                }
                else
                {
                    return string.Format("\r\nRa.C('{0}', {{ctrl:'{0}_CTRL',label:'{0}_LBL'}});", ClientID);
                }
            }
            else
            {
                if (_hasSetFocus)
                {
                    string options = "";
                    if (_hasSetFocus)
                        options += ",focus:true";
                    return string.Format("Ra.C('{0}',{{ctrl:'{0}_CTRL',label:'{0}_LBL',evts:[{2}]{1}}});", ClientID, options, evts);
                }
                else
                {
                    return string.Format("Ra.C('{0}',{{ctrl:'{0}_CTRL',label:'{0}_LBL',evts:[{1}]}});", ClientID, evts);
                }
            }
        }

        // Override this one to create specific HTML for your widgets
        public override string GetHTML()
        {
            string accessKey = string.IsNullOrEmpty(AccessKey) ? "" : string.Format(" accesskey=\"{0}\"", AccessKey);
            string groupName = string.IsNullOrEmpty(GroupName) ? string.Format(" name=\"{0}\"", ClientID) : string.Format(" name=\"{0}\"", GroupName);
            return string.Format("<span id=\"{0}\"><input type=\"radio\" value=\"{0}\" id=\"{0}_CTRL\"{2}{3}{4}{5}{6}{7} /><label id=\"{0}_LBL\" for=\"{0}_CTRL\">{1}</label></span>",
                ClientID,
                Text,
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute(),
                accessKey,
                (Enabled ? "" : "disabled=\"disabled\""),
                groupName,
                (Checked ? " checked=\"checked\"" : ""));
        }

        #endregion
    }
}