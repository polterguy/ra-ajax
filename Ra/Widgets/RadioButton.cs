/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using System.Web.UI;
using System.Web;
using Ra.Builder;

namespace Ra.Widgets
{
    /**
     * RadioButton control. This is the equivalent of the HTML input type="radio" element. By grouping
     * several of these together you can create a "single answer, multiple choice" control.
     */
    [DefaultProperty("Text")]
    [ASP.ToolboxData("<{0}:RadioButton runat=server />")]
    public class RadioButton : RaWebControl
    {
        /**
         * Raised when checked state of radiobutton is changed
         */
        public event EventHandler CheckedChanged;

        /**
         * Raised when radiobutton looses focus, opposite of Focused
         */
        public event EventHandler Blur;

        /**
         * Raised when radiobutton receives Focus, opposite of Blur
         */
        public event EventHandler Focused;

        #region [ -- Properties -- ]

        /**
         * The text that is displayed within the radiobutton, default value is string.Empty
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
         * radiobuttons with the same groupname will toggle eachother when another one is checked
         */
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

        /**
         * If true then the radiobutton is checked, otherwise it is unchecked
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
         * you can change the checked state of this radiobutton by clicking ALT+SHIFT+H on your 
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
         * If false then the radiobutton is disabled, otherwise it is enabled
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
            if (Enabled && !this.IsViewStateEnabled && ((Page)HttpContext.Current.CurrentHandler).IsPostBack)
            {
                bool valueOfChecked = 
                    ((Page)HttpContext.Current.CurrentHandler).Request.Params[
                        string.IsNullOrEmpty(GroupName) ? ClientID : GroupName] == ClientID;
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
            if (Enabled && ((Page)HttpContext.Current.CurrentHandler).IsPostBack)
            {
                bool valueOfChecked =
                    ((Page)HttpContext.Current.CurrentHandler).Request.Params[
                        string.IsNullOrEmpty(GroupName) ? ClientID : GroupName] == ClientID;
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

        // Override this one to handle events fired on the client-side
        public override void DispatchEvent(string name)
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

		protected override string GetClientSideScriptOptions()
		{
			string retVal = string.Format("ctrl:'{0}_CTRL',label:'{0}_LBL'", ClientID);
			if (_hasSetFocus)
				retVal += ",focus:true";
			return retVal;
		}

        protected override string GetMouseOutEventScript()
        {
            return string.Format("['mouseout', false, '{0}']", ClientID);
        }

        protected override string GetMouseOverEventScript()
        {
            return string.Format("['mouseover', false, '{0}']", ClientID);
        }

        protected override string GetEventsRegisterScript()
        {
            string evts = base.GetEventsRegisterScript();

            // Due to a bug in IE we need to trap click event in DOM instead of change if
            // browser is IE
            string evtChangeName = Page.Request.Browser.Browser == "IE" ? "click" : "change";
            if (CheckedChanged != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += string.Format("['{0}']", evtChangeName);
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

        protected override void RenderRaControl(HtmlBuilder builder)
        {
            using (Element span = builder.CreateElement("span"))
            {
                AddAttributes(span);
                using (Element el = builder.CreateElement("input"))
                {
                    el.AddAttribute("type", "radio");
                    el.AddAttribute("value", ClientID);
                    if (!string.IsNullOrEmpty(GroupName))
                        el.AddAttribute("name", GroupName);
                    else
                        el.AddAttribute("name", ClientID);
                    el.AddAttribute("id", ClientID + "_CTRL");
                    if (!string.IsNullOrEmpty(AccessKey))
                        el.AddAttribute("accesskey", AccessKey);
                    if (!Enabled)
                        el.AddAttribute("disabled", "disabled");
                    if (Checked)
                        el.AddAttribute("checked", "checked");
                    using (Element label = builder.CreateElement("label"))
                    {
                        label.AddAttribute("id", ClientID + "_LBL");
                        label.AddAttribute("for", ClientID + "_CTRL");
                        label.Write(Text);
                    }
                }
            }
        }

        #endregion
    }
}
