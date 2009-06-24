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

namespace Ra.Behaviors
{
    /**
     * Adds "dropping" capabilities to a Control. Tighly coupled with BehaviorDraggable. Basically
     * a draggable can be dropped onto a droppable which then will fire a "dropped onto" event in
     * addition to the draggable's dropped event. Useful for creating for instance shopping carts
     * and similar functionality which needs to track where widgets which are modified with the
     * BehaviorDraggable are actually dropped.
     */
    [ASP.ToolboxData("<{0}:BehaviorDroppable runat=\"server\" />")]
	public class BehaviorDroppable : Behavior
	{
        /**
         * EventArgs being passed into the Dropped event of the BehaviorDroppable.
         */
		public class DroppedEventArgs : EventArgs
		{
			private BehaviorDraggable _dragger;
			
			internal DroppedEventArgs(BehaviorDraggable dragger)
			{
				_dragger = dragger;
			}
		
	        /**
             * The BehaviorDraggable which was dragged. You can retrieve the control by getting the "Parent"
             * control from the draggable. This will be the control actually dragged.
             */
			public BehaviorDraggable Dragger
			{
				get { return _dragger; }
			}
		}
		
        /**
         * Fired when a BehaviorDraggable is dropped onto the BehaviorDroppable
         */
		public event EventHandler<DroppedEventArgs> Dropped;

        /**
         * A name of a CSS class which will be added to the droppable control when
         * a control with BehaviorDraggable is "on top" of it or hovering it.
         */
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

		public override string GetRegistrationScript()
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
