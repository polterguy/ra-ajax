/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using System.Drawing;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;

namespace Ra.Widgets
{
    /**
     * Will make the Control veiled by default and will be unveiled only upon mouse enter.
     */
    [ASP.ToolboxData("<{0}:BehaviorUnveiler runat=\"server\" />")]
	public class BehaviorUnveiler : Behavior
	{
        public BehaviorUnveiler()
        { }

		public override string GetRegistrationScript()
		{
            return string.Format("new Ra.BUnveil('{0}', {{}})", this.ClientID);
		}

        protected override string GetClientSideScript()
		{
			return "";
		}
    }
}
