/*
 * Ra Ajax - An Ajax Library for Mono ++
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
using Ra.Helpers;

namespace Ra.Widgets
{
    [ASP.ToolboxData("<{0}:BehaviorDroppable runat=\"server\" />")]
	public class BehaviorDroppable : Behavior
	{
		public class DroppedEventArgs : EventArgs
		{
			private BehaviorDraggable _dragger;
			
			internal DroppedEventArgs(BehaviorDraggable dragger)
			{
				_dragger = dragger;
			}
			
			public BehaviorDraggable Dragger
			{
				get { return _dragger; }
			}
		}
		
		public event EventHandler<DroppedEventArgs> Dropped;

        public string TouchedCssClass
        {
            get { return ViewState["TouchedCssClass"] == null ? "" : (string)ViewState["TouchedCssClass"]; }
            set
            {
                if (value != TouchedCssClass)
                    SetJSONValueString("TouchedCssClass", value);
                ViewState["TouchedCssClass"] = value;
            }
        }

		public override string GetRegistrationScript ()
		{
			string options = "";
			if (!string.IsNullOrEmpty(TouchedCssClass))
			{
				options = string.Format(",{{touched:\"{0}\"}}", TouchedCssClass);
			}
			return string.Format("new Ra.BDrop('{0}'{1})", this.ClientID, options);
		}

		internal void RaiseDropped(BehaviorDraggable dragger)
		{
			if (Dropped != null)
				Dropped(this, new DroppedEventArgs(dragger));
		}
	}
}
