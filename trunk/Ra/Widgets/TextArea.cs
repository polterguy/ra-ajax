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
using System.Web;
using System.Web.UI;
using Ra.Builder;

namespace Ra.Widgets
{
    /**
     * Text input control for retrieving multiple lines of text. The equivalent of the HTML 
     * textarea control. Alternative to TextBox for long strings that requires a textarea HTML 
     * element instead of TextBox that the textbox renders. Otherwise mostly the
     * same implementation as textbox.
     */
    [DefaultProperty("Text")]
    [ASP.ToolboxData("<{0}:TextBox runat=server />")]
    public class TextArea : RaWebControl
    {
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
         * Raised when Escape (Esc) is pressed when textbox has focus
         */
        public event EventHandler EscPressed;

        /**
         * Raised when control receives Focus, opposite of Blur
         */
        public event EventHandler Focused;

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
         * (X)HTML have the columns attribute on the textarea element. This is its Ra-Ajax version.
         * Basically how many characters there should be room for horizontally.
         */
        [DefaultValue(20)]
        public int Columns
        {
            get { return ViewState["Columns"] == null ? 20 : (int)ViewState["Columns"]; }
            set
            {
                if (value != Columns)
                    SetJSONGenericValue("cols", value.ToString());
                ViewState["Columns"] = value;
            }
        }

        /**
         * (X)HTML have the rows attribute on the textarea element. This is its Ra-Ajax version.
         * Basically how many characters there should be room for vertically before scrolling occurs.
         */
        [DefaultValue(2)]
        public int Rows
        {
            get { return ViewState["Rows"] == null ? 3 : (int)ViewState["Rows"]; }
            set
            {
                if (value != Rows)
                    SetJSONGenericValue("rows", value.ToString());
                ViewState["Rows"] = value;
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
            if (Enabled && !this.IsViewStateEnabled && ((Page)HttpContext.Current.CurrentHandler).IsPostBack)
            {
                string valueOfTextBox = ((Page)HttpContext.Current.CurrentHandler).Request.Params[ClientID];
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
            if (Enabled && ((Page)HttpContext.Current.CurrentHandler).IsPostBack)
            {
                string valueOfTextBox = ((Page)HttpContext.Current.CurrentHandler).Request.Params[ClientID];
                if (valueOfTextBox != Text)
				{
					// Note that to avoid the string taking up bandwidth BACK to the client
					// which it obviously does not need to do we set the ViewState value here directly instead
					// of going through the Text property which will also modify the JSON collection
                    ViewState["Text"] = valueOfTextBox;
				}
            }
        }

        // Override this one to handle events fired on the client-side
        public override void DispatchEvent(string name)
        {
            //System.Web.UI.WebControls.TextBox box;
            //box.TextMode = System.Web.UI.WebControls.TextBoxMode.
            switch (name)
            {
                case "change":
                    if (TextChanged != null)
                        TextChanged(this, new EventArgs());
                    break;
                case "esc":
                    if (EscPressed != null)
                        EscPressed(this, new EventArgs());
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
            string retVal = base.GetClientSideScriptOptions();
			if (_hasSetSelect)
			{
                if (!string.IsNullOrEmpty(retVal))
					retVal += ",";
				retVal += "select:true";
			}
			return retVal;
		}

        protected override string GetEventsRegisterScript()
        {
            string evts = base.GetEventsRegisterScript();
            if (TextChanged != null)
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
            if (EscPressed != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['esc']";
            }
            return evts;
        }

        protected override void RenderRaControl(HtmlBuilder builder)
        {
            using (Element el = builder.CreateElement("textarea"))
            {
                AddAttributes(el);
                el.Write(Text);
            }
        }

        protected override void AddAttributes(Element el)
        {
            el.AddAttribute("name", ClientID);
            el.AddAttribute("rows", Rows.ToString());
            el.AddAttribute("cols", Columns.ToString());
            if (!string.IsNullOrEmpty(AccessKey))
                el.AddAttribute("accesskey", AccessKey);
            if (!Enabled)
                el.AddAttribute("disabled", "disabled");
            base.AddAttributes(el);
        }

        #endregion
    }
}
