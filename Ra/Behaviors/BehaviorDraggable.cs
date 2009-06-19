/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 * Authors: 
 * Thomas Hansen (thomas@ra-ajax.org)
 * Kariem Ali (kariem@ra-ajax.org)
 * 
 */

using System;
using System.ComponentModel;
using System.Drawing;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Widgets;

namespace Ra.Behaviors
{
    /**
     * Adds dragging capabilities to your Ajax controls. Add this to the Control Collection of any of
     * your widget and your widget can be dragged and dropped around on screen. When Widget is dropped the
     * Dropped event will be raised and the widget will have updated left and top style collection values.
     */
    [ASP.ToolboxData("<{0}:BehaviorDraggable runat=\"server\" />")]
    public class BehaviorDraggable : Behavior, IRaControl, IDisableableBehavior
	{
        /**
         * Event raised when control has been dropped into a new location on form
         */
		public event EventHandler Dropped;

        /**
         * min/max dragging bounds of control
         */
        public Rectangle Bounds
        {
            get { return ViewState["Bounds"] == null ? Rectangle.Empty : (Rectangle)ViewState["Bounds"]; }
            set
            {
                if (value != Bounds)
                    SetJSONValueObject("Bounds", value);
                ViewState["Bounds"] = value;
            }
        }

        /**
         * "resolution" which the control should snap into. Think of it like a grid.
         */
        public Point Snap
        {
            get { return ViewState["Snap"] == null ? Point.Empty : (Point)ViewState["Snap"]; }
            set
            {
                if (value != Snap)
                    SetJSONValueObject("Snap", value);
                ViewState["Snap"] = value;
            }
        }

        /**
         * If given it is expected to be the id of a DOM element from which the control will be draggable
         * from. Then control can only be dragged by clicking this specific DOM element. Doesn't
         * have to be the DOM element of a Control.
         */
        public string Handle
        {
            get { return ViewState["Handle"] == null ? "" : (string)ViewState["Handle"]; }
            set
            {
                if (value != Handle)
                    SetJSONValueString("Handle", value);
                ViewState["Handle"] = value;
            }
        }

		public override string GetRegistrationScript()
		{
			string options = string.Empty;
			if( Bounds != Rectangle.Empty)
			{
				options += string.Format(",{{bounds:{{left:{0},top:{1},width:{2},height:{3}}}",
                    Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height);
			}
			if( Snap != Point.Empty)
			{
				if( options != string.Empty)
					options += ",";
				else
					options += ",{";
				options += string.Format("snap:{{x:{0},y:{1}}}", Snap.X, Snap.Y);
			}
			if( !string.IsNullOrEmpty(Handle))
			{
				if( options != string.Empty)
					options += ",";
				else
					options += ",{";
				options += string.Format("handle:Ra.$('{0}')", Handle);
			}

            if (options != string.Empty)
                options += ",";
            else
                options += ",{";
            options += string.Format("enabled:{0}", Enabled.ToString().ToLowerInvariant());

			if( options != string.Empty)
				options += "}";
			return string.Format("new Ra.BDrag('{0}'{1})", this.ClientID, options);
		}

        protected override string GetClientSideScript()
		{
			return "";
		}

        internal override void EnsureViewStateLoads()
        {
            if (((System.Web.UI.Page)System.Web.HttpContext.Current.CurrentHandler).Request.Params["__RA_CONTROL"] == this.ClientID &&
                ((System.Web.UI.Page)System.Web.HttpContext.Current.CurrentHandler).Request.Params["__EVENT_NAME"] == "dropped")
            {
                RaWebControl parent = Parent as RaWebControl;
                parent.Style.SetStyleValueViewStateOnly("left", Page.Request.Params["x"] + "px");
                parent.Style.SetStyleValueViewStateOnly("top", Page.Request.Params["y"] + "px");
                parent.Style.SetStyleValueViewStateOnly("position", "absolute");
            }
        }

        void IRaControl.DispatchEvent(string name)
        {
            switch (name)
            {
                case "dropped":
                    if (Dropped != null)
                        Dropped(this, new EventArgs());
				    string drops = Page.Request.Params["drops"];
				    if( !string.IsNullOrEmpty(drops) )
					{
					    string[] affectedDroppersIds = drops.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
					    foreach( string idx in affectedDroppersIds)
						{
                            BehaviorDroppable tmp = AjaxManager.Instance.FindControlClientID<BehaviorDroppable>(idx);
					        tmp.RaiseDropped(this);
						}
					}
                    break;
                default:
                    throw new ApplicationException("Unknown event fired for control");
            }
        }

        #region IDisableableBehavior Members

        /**
         * Determines whether a Behviour that implements IDisableableBehvior is enabled or disabled
         */ 
        [DefaultValue(true)]
        public bool Enabled
        {
            get { return ViewState["Enabled"] == null ? true : (bool)ViewState["Enabled"]; }
            set
            {
                if (value != Enabled)
                    SetJSONValueBool("Enabled", value);
                ViewState["Enabled"] = value;
            }
        }

        #endregion
    }
}
