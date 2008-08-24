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
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;

[assembly: ASP.WebResource("Extensions.RichEdit.js", "text/javascript")]

namespace Ra.Extensions
{
    [ASP.ToolboxData("<{0}:RichEdit runat=server></{0}:RichEdit>")]
    public class RichEdit : RaWebControl
    {
        public event EventHandler KeyUp;

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

        private string _selection;
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
            AjaxManager.Instance.IncludeScriptFromResource(typeof(Timer), "Extensions.RichEdit.js");
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

        public override void DispatchEvent(string name)
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

        private bool _scriptRetrieved;
        public override string GetClientSideScript()
        {
            if (_scriptRetrieved)
                return "";
            _scriptRetrieved = true;
            string evts = "";
            if (KeyUp != null)
                evts += "[['keyup']]";
            if (evts.Length > 0)
                evts = "evts:" + evts;
            if (_hasSetFocus)
            {
                return string.Format("\r\nnew Ra.RichEdit('{0}', {{focus:true,{1}}});",
                    ClientID,
                    evts);
            }
            else
            {
                return string.Format("\r\nnew Ra.RichEdit('{0}', {{{1}}});",
                    ClientID,
                    evts);
            }
        }

        public override string GetHTML()
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
