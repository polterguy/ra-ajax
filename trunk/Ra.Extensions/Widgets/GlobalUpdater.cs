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
using System.Collections.Generic;
using System.Globalization;

[assembly: ASP.WebResource("Extensions.Js.GlobalUpdater.js", "text/javascript")]

namespace Ra.Extensions.Widgets
{
    /**
     * A GlobalUpdater is an updater which will be for your entire page. This means that
     * all Ajax Callbacks that are being created and initiated will be using this
     * control as their 'wait.gif' control. This is useful for scenarios where you might 
     * have Ajax Callbacks that may take a long time, but they're not associated with 
     * any particular control(s) and hence the BehaviorUpdater is not that useful.
     */
    [ASP.ToolboxData("<{0}:GlobalUpdater runat=\"server\"></{0}:GlobalUpdater>")]
    public class GlobalUpdater : Panel, ASP.INamingContainer
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Style[Styles.display] = "none";
        }

        /**
         * Number of milliseconds which will pass before the obscurer will animate into view
         * and show up. If the request is going fast then by having a sane value for this property
         * you can make sure that only the "slow" requests are actually running the updater logic
         * and obscuring the page.
         */
        [DefaultValue(1000)]
        public int Delay
        {
            get { return ViewState["Delay"] == null ? 1000 : (int)ViewState["Delay"]; }
            set
            {
                if (value != Delay)
                    SetJSONValueObject("Delay", value);
                ViewState["Delay"] = value;
            }
        }

        /**
         * When the Updater is being shown it will be faded into view. The default full opacity
         * value for the updater is 100% which equals to 1.0 for this value. By setting this
         * value to any other value you can however override the maximum opacity the updater
         * will fade into. 0.0 as the value for this property will not show the Updater at all.
         */
        [DefaultValue(1.0)]
        public decimal MaxOpacity
        {
            get { return ViewState["MaxOpacity"] == null ? 1.0M : (decimal)ViewState["MaxOpacity"]; }
            set
            {
                if (value != MaxOpacity)
                    SetJSONValueObject("MaxOpacity", value);
                ViewState["MaxOpacity"] = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            AjaxManager.Instance.IncludeScriptFromResource(typeof(GlobalUpdater), "Extensions.Js.GlobalUpdater.js");
            base.OnPreRender(e);
        }

        protected override string GetClientSideScriptOptions()
        {
            string retVal = base.GetClientSideScriptOptions();
            if (Delay != 1000)
            {
                if (retVal != string.Empty)
                    retVal += ",";
                retVal += string.Format("delay:{0}", Delay);
            }
            if (MaxOpacity != 1.0M)
            {
                if (retVal != string.Empty)
                    retVal += ",";
                retVal += string.Format("maxOpacity:{0}", MaxOpacity.ToString(CultureInfo.InvariantCulture));
            }
            return retVal;
        }

        protected override string GetClientSideScriptType()
        {
            return "new Ra.GlobalUpdater";
        }
    }
}
