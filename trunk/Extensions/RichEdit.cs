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
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;

[assembly: ASP.WebResource("Extensions.RichEdit.js", "text/javascript")]

namespace Ra.Extensions
{
    [ASP.ToolboxData("<{0}:RichEdit runat=server></{0}:RichEdit>")]
    public class RichEdit : RaWebControl
    {
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            AjaxManager.Instance.IncludeScriptFromResource(typeof(Timer), "Extensions.RichEdit.js");
        }

        private bool _scriptRetrieved;
        public override string GetClientSideScript()
        {
            if (_scriptRetrieved)
                return "";
            _scriptRetrieved = true;
            return string.Format("\r\nnew Ra.RichEdit('{0}', {{label:'{0}_LBL'}});", 
                ClientID);
        }

        public override string GetHTML()
        {
            // Rich Edit DIV editing element plus hidden field which is "value" part 
            // to make sure we submit the new value back to server whan changes occurs...
            // In additiont we've also got a hidden input field which serves as the "selected text"
            // field and contains the value which is currently selected in the RichEdit...
            return string.Format("<div id=\"{0}\"{2}{3}><div id=\"{0}_LBL\">{1}</div><input type=\"hidden\" id=\"{0}__VALUE\" /><input type=\"hidden\" id=\"{0}__SELECTED\" /></div>",
                ClientID,
                Text,
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute());
        }
    }
}
