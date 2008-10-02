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

namespace Ra.Widgets
{
    /**
     * Textbox control, maps to the &lt;input type="text" HTML element. If you need a multi column textbox
     * you should rather use the TextAre control.
     */
    [DefaultProperty("Text")]
    [ASP.ToolboxData("<{0}:TextBox runat=server />")]
    public class TextBox : RaWebControl, IRaControl
    {
        /**
         * Type of TextBox
         */
        public enum TextBoxMode
        {
            /**
             * Default, will display a normal TextBox
             */
            SingleLine,

            /**
             * Will display a password TextBox which will obscur the characters typed or pasted into it
             */
            Password
        };

        private bool _hasSetSelect;

        /**
         * Raised when text value of control is changed
         */
        public event EventHandler TextChanged;

        /**
         * Raised when control looses focus, opposite of Focused
         */
        public event EventHandler Blur;

        /**
         * Raised when control receives Focus, opposite of Blur
         */
        public event EventHandler Focused;

        /**
         * Raised when mouse is over the control, opposite of MouseOut
         */
        public event EventHandler MouseOver;

        /**
         * Raised when mouse is leaving the control, opposite of MouseOver
         */
        public event EventHandler MouseOut;

        /**
         * Raised when JS DOM event keyup is raised on client. Basically when a key is pressed and released.
         */
        public event EventHandler KeyUp;

        #region [ -- Properties -- ]

        /**
         * The text that is displayed within the control, default value is string.Empty
         */
        [DefaultValue("")]
        public string Text
        {
            get { return ViewState["Text"] == null ? "" : (string)ViewState["Text"]; }
            set
            {
                if (value != Text)
                    SetJSONValueString("Value", value);
                ViewState["Text"] = value;
            }
        }

        /**
         * The keyboard shortcut for clicking the button. Most browsers implements
         * some type of keyboard shortcut logic like for instance FireFox allows
         * form elements to be triggered by combining the AccessKey value (single character)
         * together with ALT and SHIFT. Meaning if you have e.g. "H" as keyboard shortcut
         * you can click this button by doing ALT+SHIFT+H on your keyboard. The combinations
         * to effectuate the keyboard shortcuts however vary from browsers to browsers.
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
         * If false then the button is disabled, otherwise it is enabled
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

        /**
         * Kind of textbox to render
         */
        [DefaultValue(TextBox.TextBoxMode.SingleLine)]
        public TextBoxMode TextMode
        {
            get { return ViewState["TextMode"] == null ? TextBoxMode.SingleLine : (TextBoxMode)ViewState["TextMode"]; }
            set
            {
                if (value != TextMode)
                    SetJSONValueString("Type", value == TextBoxMode.Password ? "text" : "password");
                ViewState["TextMode"] = value;
            }
        }

        #endregion

        /**
         * Will if called select all text within control
         */
        public void Select()
        {
            _hasSetSelect = true;
            if (AjaxManager.Instance.IsCallback)
            {
                SetJSONValueString("Select", "");
            }
        }

        #region [ -- Overridden (abstract/virtual) methods from RaControl -- ]

        protected override void OnInit(EventArgs e)
        {
            // Since if ViewState is DISABLED we will NEVER come into LoadViewState we need to
            // have the same logic in OnInit since we really should modify the Text value to
            // the postback value BEFORE Page_Load event is fired...
            if (Enabled && !this.IsViewStateEnabled && AjaxManager.Instance.CurrentPage.IsPostBack)
            {
                string valueOfTextBox = AjaxManager.Instance.CurrentPage.Request.Params[ClientID];
                if (valueOfTextBox != Text)
				{
					// Note that to avoid the string taking up bandwidth BACK to the client
					// which it obviously does not need to do we set the ViewState value here directly instead
					// of going through the Text property which will also modify the JSON collection
                    ViewState["Text"] = valueOfTextBox;
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
                string valueOfTextBox = AjaxManager.Instance.CurrentPage.Request.Params[ClientID];
                if (valueOfTextBox != Text)
				{
					// Note that to avoid the string taking up bandwidth BACK to the client
					// which it obviously does not need to do we set the ViewState value here directly instead
					// of going through the Text property which will also modify the JSON collection
                    ViewState["Text"] = valueOfTextBox;
				}
            }
        }

        void IRaControl.DispatchEvent(string name)
        {
            //System.Web.UI.WebControls.TextBox box;
            //box.TextMode = System.Web.UI.WebControls.TextBoxMode.
            switch (name)
            {
                case "change":
                    if (TextChanged != null)
                        TextChanged(this, new EventArgs());
                    break;
                case "mouseover":
                    if (MouseOver != null)
                        MouseOver(this, new EventArgs());
                    break;
                case "mouseout":
                    if (MouseOut != null)
                        MouseOut(this, new EventArgs());
                    break;
                case "keyup":
                    if (KeyUp != null)
                        KeyUp(this, new EventArgs());
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
			string retVal = string.Empty;

			if (_hasSetFocus)
				retVal += "focus:true";
			if (_hasSetSelect)
			{
				if (retVal.Length != 0)
					retVal += ",";
				retVal += "select:true";
			}
			return retVal;
		}

        protected override string GetEventsRegisterScript()
        {
            string evts = string.Empty;
            if (TextChanged != null)
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
            if (KeyUp != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['keyup']";
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

        protected override string GetOpeningHTML()
        {
            string accessKey = string.IsNullOrEmpty(AccessKey) ? "" : string.Format(" accesskey=\"{0}\"", AccessKey);
            return string.Format("<input type=\"{5}\" id=\"{0}\" name=\"{0}\" value=\"{1}\"{2}{3}{4}{6} />",
                ClientID,
                Text,
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute(),
                accessKey,
                (TextMode == TextBoxMode.SingleLine ? "text" : "password"),
                (Enabled ? "" : " disabled=\"disabled\""));
        }

        #endregion
    }
}
