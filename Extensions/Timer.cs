/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Ra Software AS thomas@ra-ajax.org
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

[assembly: ASP.WebResource("Extensions.Timer.js", "text/javascript")]

namespace Ra.Extensions
{
    [ASP.ToolboxData("<{0}:Timer runat=server />")]
    public class Timer : RaControl
    {
        public event EventHandler Tick;

        [DefaultValue(true)]
        public bool Enabled
        {
            get { return ViewState["Enabled"] == null ? true : (bool)ViewState["Enabled"]; }
            set
            {
                if (value != Enabled)
                    SetJSONValueBool("Enabled", value);
                ViewState["Enabled"] = value;
            }
        }

        [DefaultValue(1000)]
        public int Milliseconds
        {
            get { return ViewState["Milliseconds"] == null ? 1000 : (int)ViewState["Milliseconds"]; }
            set
            {
                ViewState["Milliseconds"] = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            AjaxManager.Instance.IncludeScriptFromResource(typeof(Timer), "Extensions.Timer.js");
        }

        public override void DispatchEvent(string name)
        {
            switch (name)
            {
                case "tick":
                    if (Tick != null)
                        Tick(this, new EventArgs());
                    break;
            }
        }

        private bool _scriptRetrieved;
        public override string GetClientSideScript()
        {
            if (_scriptRetrieved)
                return "";
            _scriptRetrieved = true;
            return string.Format("\r\nnew Ra.Timer('{0}', {{enabled:{1}, duration:{2}}});", 
                ClientID,
                (Enabled && Tick != null).ToString().ToLower(),
                Milliseconds);
        }

        public override string GetHTML()
        {
            // Dummy HTML DOM element to make registration and such easier...
            return string.Format("<span style=\"display:none;\" id=\"{0}\">&nbsp;</span>", ClientID);
        }
    }
}
