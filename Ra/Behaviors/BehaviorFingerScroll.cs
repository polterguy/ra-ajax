/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using System.ComponentModel;
using System.Drawing;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Widgets;
using System.Web;

namespace Ra.Behaviors
{
    /**
     * Makes your widgets scrollable on iPhone and iPad through the native common interface
     */
    [ASP.ToolboxData("<{0}:BehaviorFingerScroll runat=\"server\" />")]
    public class BehaviorFingerScroll : Behavior, IRaControl
	{
		public override string GetRegistrationScript()
		{
            return string.Format("new Ra.BFScroll('{0}')", this.ClientID);
		}

        protected override string GetClientSideScript()
		{
			return "";
		}

        public void DispatchEvent(string name)
        {
        }
    }
}
