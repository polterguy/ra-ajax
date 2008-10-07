/*
 * Ra-Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;

namespace Ra.Widgets
{
    /**
     * CheckBox control, renders &lt;input type="checkbox"...
     */
    [DefaultProperty("Text")]
    [ASP.ToolboxData("<{0}:CheckBox runat=server />")]
    public class CheckBox : RaWebControl, IRaControl
    {
        /**
         * Raised when checked state of checkbox is changed
         */
        public event EventHandler CheckedChanged;

        /**
         * Raised when checkbox looses focus, opposite of Focused
         */
        public event EventHandler Blur;

        /**
         * Raised when checkbox receives Focus, opposite of Blur
         */
        public event EventHandler Focused;

        /**
         * Raised when mouse is over the checkbox, opposite of MouseOut
         */
        public event EventHandler MouseOver;

        /**
         * Raised when mouse is leaving the checkbox, opposite of MouseOver
         */
        public event EventHandler MouseOut;

        #region [ -- Properties -- ]

        /**
         * The text that is associated with the checkbox, normally rendered to the right.
         * Default value is string.Empty
         */
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

        /**
         * If true then the checkbox is checked, otherwise it is unchecked
         */
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

        /**
         * The keyboard shortcut for changing the checked state. Most browsers implements
         * some type of keyboard shortcut logic like for instance FireFox allows
         * form elements to be triggered by combining the AccessKey value (single character)
         * together with ALT and SHIFT. Meaning if you have e.g. "H" as keyboard shortcut
         * you can change the checked state of this checkbox by clicking ALT+SHIFT+H on your 
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

        #endregion

        protected override void OnInit(EventArgs e)
        {
            // Since if ViewState is DISABLED we will NEVER come into LoadViewState we need to
            // have the same logic in OnInit since we really should modify the Text value to
            // the postback value BEFORE Page_Load event is fired...
            if (Enabled && !this.IsViewStateEnabled && AjaxManager.Instance.CurrentPage.IsPostBack)
            {
                bool valueOfChecked = AjaxManager.Instance.CurrentPage.Request.Params[ClientID] == "on";
                if (valueOfChecked != Checked)
				{
					// Note that to avoid the string taking up bandwidth BACK to the client
					// which it obviously does not need to do we set the ViewState value here directly instead
					// of going through the Checked property which will also modify the JSON collection
                    ViewState["Checked"] = valueOfChecked;
				}
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
                bool valueOfChecked = AjaxManager.Instance.CurrentPage.Request.Params[ClientID] == "on";
                if (valueOfChecked != Checked)
				{
					// Note that to avoid the string taking up bandwidth BACK to the client
					// which it obviously does not need to do we set the ViewState value here directly instead
					// of going through the Checked property which will also modify the JSON collection
                    ViewState["Checked"] = valueOfChecked;
				}
            }
        }

        #region [ -- Overridden (abstract/virtual) methods from RaControl -- ]

        void IRaControl.DispatchEvent(string name)
        {
            switch (name)
            {
                // Due to a bug in IE we need to trap click event in DOM instead of change if
                // browser is IE
                case "click":
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

		protected override string GetClientSideScriptOptions()
		{
			string retVal = string.Format("ctrl:'{0}_CTRL',label:'{0}_LBL'", ClientID);
			if (_hasSetFocus)
				retVal += ",focus:true";
			return retVal;
		}

        protected override string GetEventsRegisterScript()
        {
            string evts = string.Empty;

            // Due to a bug in IE we need to trap click event in DOM instead of change if
            // browser is IE
            string evtChangeName = Page.Request.Browser.Browser == "IE" ? "click" : "change";
            if (CheckedChanged != null)
                evts += string.Format("['{0}']", evtChangeName);
            if (MouseOver != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += string.Format("['mouseover', false, '{0}_LBL']", ClientID);
            }
            if (MouseOut != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += string.Format("['mouseout', false, '{0}_LBL']", ClientID);
            }
            if (Blur != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += string.Format("['blur', false, '{0}_CTRL']", ClientID);
            }
            if (Focused != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += string.Format("['focus', false, '{0}_CTRL']", ClientID);
            }
			return evts;
        }

        protected override string GetOpeningHTML()
        {
            string accessKey = string.IsNullOrEmpty(AccessKey) ? "" : string.Format(" accesskey=\"{0}\"", AccessKey);
            return string.Format("<span id=\"{0}\"><input type=\"checkbox\" name=\"{0}\" id=\"{0}_CTRL\"{2}{3}{4}{5}{6} /><label id=\"{0}_LBL\" for=\"{0}_CTRL\">{1}</label></span>",
                ClientID,
                Text,
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute(),
                accessKey,
                (Enabled ? "" : " disabled=\"disabled\""),
                (Checked ? " checked=\"checked\"" : ""));
        }

        #endregion
    }
}
