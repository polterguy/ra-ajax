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

namespace Ra.Widgets
{
	public abstract class Behavior : RaControl, IRaControl
	{
	    protected override void OnInit(EventArgs e)
	    {
	        base.OnInit(e);
			AjaxManager.Instance.IncludeScriptFromResource("Behaviors.js");
	    }
		
		public override string GetHTML()
		{
			return string.Empty;
		}

        public override void RenderControl(ASP.HtmlTextWriter writer)
        {
            if (DesignMode)
                throw new ApplicationException("Ra Ajax doesn't support Design time");

			// We roughly only needs to handle what happens for JSON changes in the Behaviors
            if (Visible)
            {
                if (AjaxManager.Instance.IsCallback)
                {
                    if (Phase == RenderingPhase.Visible)
                    {
                        // JSON changes, control was visible also previous request...
                        string JSON = SerializeJSON();
                        if (JSON != null)
                        {
                            AjaxManager.Instance.Writer.WriteLine("Ra.Beha.$('{0}').handleJSON({1});",
                                ClientID,
                                JSON);
                        }
                        RenderChildren(writer);
                    }
                }
            }
        }

		public abstract string GetRegistrationScript();
	}
}
