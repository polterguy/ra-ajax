/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
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
    [ASP.ToolboxData("<{0}:BehaviorObscurable runat=\"server\" />")]
    public class BehaviorObscurable : Behavior
	{
		public override string GetRegistrationScript()
		{
			return string.Format("new Ra.BObscur('{0}')", this.ClientID);
		}

        protected override string GetClientSideScript()
		{
			return "";
		}
	}
}
