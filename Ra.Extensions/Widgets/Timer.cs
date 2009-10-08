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
using Ra.Builder;

[assembly: ASP.WebResource("Ra.Extensions.Js.Timer.js", "text/javascript")]

namespace Ra.Extensions.Widgets
{
    /**
     * Ajax timer, raises its Tick event handler back to server periodically. Alternative to Comet 
     * component and far "safer" to use than Comet. Think of it as "polling the server to see if updates
     * have arrived".
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
        }

        protected override void OnPreRender(EventArgs e)
        {
            AjaxManager.Instance.IncludeScriptFromResource(typeof(Timer), "Ra.Extensions.Js.Timer.js");
            base.OnPreRender(e);
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
            {
                if (!string.IsNullOrEmpty(retVal))
                    retVal += ",";
                retVal += "enabled:true";
            }
			if (Duration != 1000)
			{
                if (!string.IsNullOrEmpty(retVal))
					retVal += ",";
				retVal += string.Format("duration:{0}", Duration);
			}
			return retVal;
		}

		protected override string GetClientSideScriptType()
		{
			return "new Ra.Timer";
		}

        protected override void RenderRaControl(HtmlBuilder builder)
        {
            using (Element el = builder.CreateElement("span"))
            {
                AddAttributes(el);
                el.Write("&nbsp;");
            }
        }

        protected override void AddAttributes(Element el)
        {
            el.AddAttribute("style", "display:none;");
            base.AddAttributes(el);
        }
    }
}
