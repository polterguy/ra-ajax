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
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web;
using Ra.Builder;

[assembly: ASP.WebResource("Ra.Extensions.Js.RichEdit.js", "text/javascript")]

namespace Ra.Extensions.Widgets
{
    /**
     * TextBox control with rich formatting capabilities (HTML). Notice though that this control is 
     * NOT STABLE and is only included for research reasons and will be broken in future version of
     * Ra-Ajax.
     */
    [ASP.ToolboxData("<{0}:RichEdit runat=server></{0}:RichEdit>")]
    public class RichEdit : RaWebControl, IRaControl
    {
        private string _selection;

        /**
         * Ke pressed and released
         */
        public new event EventHandler KeyUp;

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
            if (!this.IsViewStateEnabled && ((Page)HttpContext.Current.CurrentHandler).IsPostBack && this.Visible)
            {
                // Making sure we get our NEW value loaded...
                string value = Page.Request.Params[ClientID + "__VALUE"];
                ViewState["Text"] = value;

                _selection = Page.Request.Params[ClientID + "__SELECTED"];
            }
            base.OnInit(e);
            AjaxManager.Instance.IncludeScriptFromResource(typeof(Timer), "Ra.Extensions.Js.RichEdit.js");
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            if (((Page)HttpContext.Current.CurrentHandler).IsPostBack && this.Visible)
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

        protected override void RenderRaControl(HtmlBuilder builder)
        {
            using (Element el = builder.CreateElement("div"))
            {
                AddAttributes(el);
                using (Element txt = builder.CreateElement("div"))
                {
                    txt.AddAttribute("id", ClientID + "_LBL");
                    txt.Write(Text);
                }
                using (Element value = builder.CreateElement("input"))
                {
                    value.AddAttribute("type", "hidden");
                    value.AddAttribute("id", ClientID + "__VALUE");
                    value.AddAttribute("name", ClientID + "__VALUE");
                }
                using (Element sel = builder.CreateElement("input"))
                {
                    sel.AddAttribute("type", "hidden");
                    sel.AddAttribute("id", ClientID + "__SELECTED");
                    sel.AddAttribute("name", ClientID + "__SELECTED");
                }
            }
        }
    }
}
