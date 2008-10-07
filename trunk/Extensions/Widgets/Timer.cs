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
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;

[assembly: ASP.WebResource("Extensions.Js.Timer.js", "text/javascript")]

namespace Ra.Extensions
{
    /**
     * Ajax timer, raises Tick evnt handler back to server periodically. Alternative to Comet component and
     * far "safer" to use than Comet
     */
    [ASP.ToolboxData("<{0}:Timer runat=\"server\" />")]
    public class Timer : RaControl, IRaControl
    {
        /**
         * Raised periodically every Duration milliseconds
         */
        public event EventHandler Tick;

        /**
         * if true control is enabled and will raise the Tick events every Duration milliseconds, otherwise
         * will not raise tick events before enabled again
         */
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

        /**
         * Milliseconds bewteen Tick events are raised
         */
        [DefaultValue(1000)]
        public int Duration
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
            AjaxManager.Instance.IncludeScriptFromResource(typeof(Timer), "Extensions.Js.Timer.js");
        }

        void IRaControl.DispatchEvent(string name)
        {
            switch (name)
            {
                case "tick":
                    if (Tick != null)
                        Tick(this, new EventArgs());
                    break;
            }
        }

		protected override string GetClientSideScriptOptions()
		{
			string retVal = base.GetClientSideScriptOptions();
			if (Enabled && Tick != null)
				retVal += "enabled:true";
			if (Duration != 1000)
			{
				if (retVal != string.Empty)
					retVal += ",";
				retVal += string.Format("duration:{0}", Duration);
			}
			return retVal;
		}

		protected override string GetClientSideScriptType()
		{
			return "new Ra.Timer";
		}

        protected override string GetOpeningHTML()
        {
            // Dummy HTML DOM element to make registration and such easier...
            return string.Format("<span style=\"display:none;\" id=\"{0}\">&nbsp;</span>", ClientID);
        }
    }
}
