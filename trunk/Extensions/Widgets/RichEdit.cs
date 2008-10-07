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
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;

[assembly: ASP.WebResource("Extensions.Js.RichEdit.js", "text/javascript")]

namespace Ra.Extensions
{
    /**
     * TextBox control with rich formatting capabilities (HTML)
     */
    [ASP.ToolboxData("<{0}:RichEdit runat=server></{0}:RichEdit>")]
    public class RichEdit : RaWebControl, IRaControl
    {
        private string _selection;

        /**
         * Ke pressed and released
         */
        public event EventHandler KeyUp;

        /**
         * text (HTML) of control
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
         * Selected text/HTML of control. Contains formatting
         */
        [Browsable(false)]
        public string Selection
        {
            get { return _selection; }
            set
            {
                if (value != Selection)
                {
                    _selection = value;
                    SetJSONValueString("Paste", value);
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (!this.IsViewStateEnabled && AjaxManager.Instance.CurrentPage.IsPostBack && this.Visible)
            {
                // Making sure we get our NEW value loaded...
                string value = Page.Request.Params[ClientID + "__VALUE"];
                ViewState["Text"] = value;

                _selection = Page.Request.Params[ClientID + "__SELECTED"];
            }
            base.OnInit(e);
            AjaxManager.Instance.IncludeScriptFromResource(typeof(Timer), "Extensions.Js.RichEdit.js");
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            if (AjaxManager.Instance.CurrentPage.IsPostBack && this.Visible)
            {
                // Making sure we get our NEW value loaded...
                string value = Page.Request.Params[ClientID + "__VALUE"];
                ViewState["Text"] = value;

                _selection = Page.Request.Params[ClientID + "__SELECTED"];
            }
        }

        void IRaControl.DispatchEvent(string name)
        {
            switch (name)
            {
                case "keyup":
                    if (KeyUp != null)
                        KeyUp(this, new EventArgs());
                    break;
                default:
                    throw new ArgumentException("Unknown event sent to RichEdit");
            }
        }

		protected override string GetClientSideScriptType()
		{
			return "new Ra.RichEdit";
		}
		
		protected override string GetEventsRegisterScript()
		{
            string evts = string.Empty;
            if (KeyUp != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['keyup']";
            }
			return evts;
		}

        protected override string GetOpeningHTML()
        {
            // Rich Edit DIV editing element plus hidden field which is "value" part 
            // to make sure we submit the new value back to server whan changes occurs...
            // In additiont we've also got a hidden input field which serves as the "selected text"
            // field and contains the value which is currently selected in the RichEdit...
            return string.Format("<div id=\"{0}\"{2}{3}><div id=\"{0}_LBL\">{1}</div><input type=\"hidden\" id=\"{0}__VALUE\" name=\"{0}__VALUE\" /><input type=\"hidden\" id=\"{0}__SELECTED\" name=\"{0}__SELECTED\" /></div>",
                ClientID,
                Text,
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute());
        }
    }
}
