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

		public override string GetRegistrationScript ()
		{
			return string.Format("new Ra.BDrop('{0}')", this.ClientID);
		}

        public override void DispatchEvent(string name)
        {
            switch (name)
            {
                default:
                    throw new ApplicationException("Unknown event fired for control");
            }
        }
		
		internal void RaiseDropped(BehaviorDraggable dragger)
		{
			if (Dropped != null)
				Dropped(this, new DroppedEventArgs(dragger));
		}
	}
}
