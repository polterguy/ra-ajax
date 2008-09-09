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

        public override void RenderControl(System.Web.UI.HtmlTextWriter writer)
        { }

		public abstract string GetRegistrationScript();
	}
}
