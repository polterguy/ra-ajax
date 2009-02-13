/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
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
     * Abstract base class for all behaviors. A behavior is an extra capability you can assign to an
     * Ajax control. Use by adding to the Controls collection of your controls.
     */
	public abstract class Behavior : RaControl
	{
	    protected override void OnInit(EventArgs e)
	    {
	        base.OnInit(e);
#if !CONCAT
			AjaxManager.Instance.IncludeScriptFromResource("Behaviors.js");
#endif
	    }

        protected override string GetOpeningHTML()
		{
			return string.Empty;
		}

        public override void RenderControl(ASP.HtmlTextWriter writer)
        {
			// We roughly only needs to handle what happens for JSON changes in the Behaviors
            if (!DesignMode)
            {
                if (Visible)
                {
                    if (AjaxManager.Instance.IsCallback)
                    {
                        if (this.HasRendered)
                        {
                            RenderOnlyJSON(writer);
                        }
                    }
                }
            }
        }

        /**
         * Override this one to return the client-side registration script which will be injected
         * into the control registration script
         */
		public abstract string GetRegistrationScript();
    }
}
