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
    [ASP.ToolboxData("<{0}:BehaviorDraggable runat=server />")]
	public class BehaviorDraggable : Behavior
	{
		public event EventHandler Dropped;

		public override string GetRegistrationScript ()
		{
			return string.Format("new Ra.BDrag('{0}')", this.ClientID);
		}

        public override void DispatchEvent(string name)
        {
            switch (name)
            {
                case "dropped":
                    RaWebControl parent = Parent as RaWebControl;
				    parent.Style["left"] = Page.Request.Params["x"] + "px";
				    parent.Style["top"] = Page.Request.Params["y"] + "px";
                    if (Dropped != null)
                        Dropped(this, new EventArgs());
                    break;
                default:
                    throw new ApplicationException("Unknown event fired for control");
            }
        }
	}
}
