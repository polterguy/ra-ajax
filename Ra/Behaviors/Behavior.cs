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

namespace Ra.Behaviors
{
    /**
     * Abstract base class for all behaviors. A behavior is an extra capability you can assign to an
     * Ajax control. Use by adding to the Controls collection of your controls. A Behavior can be many
     * things. It can give your Widget drag and drop capabilities. Or it can make your widget fade in and
     * out when mouse is moved over etc. It is also very easy to create your own behaviors if you're 
     * knowledged in JavaScript.
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

        internal virtual void EnsureViewStateLoads()
        { }

        protected override string GetOpeningHTML()
		{
			return string.Empty;
		}

        public override void RenderControl(ASP.HtmlTextWriter writer)
        {
			// We roughly only need to handle what happens for JSON changes in the Behaviors
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

        internal override void RenderOnlyJSON(System.Web.UI.HtmlTextWriter writer)
        {
            string JSON = SerializeJSON();
            if (!string.IsNullOrEmpty(JSON))
            {
                AjaxManager.Instance.Writer.WriteLine("Ra.Beha.$('{0}').handleJSON({1});", ClientID, JSON);
            }
        }

        /**
         * Override this one to return the client-side registration script which will be injected
         * into the control registration script
         */
		public abstract string GetRegistrationScript();
    }
}
