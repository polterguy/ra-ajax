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

namespace Ra.Widgets
{
    [ASP.ToolboxData("<{0}:BehaviorDraggable runat=\"server\" />")]
    public class BehaviorDraggable : Behavior, IRaControl
	{
		public event EventHandler Dropped;

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

		public override string GetRegistrationScript ()
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
			if( options != string.Empty)
				options += "}";
			return string.Format("new Ra.BDrag('{0}'{1})", this.ClientID, options);
		}

        protected override string GetClientSideScript()
		{
			return "";
		}

        void IRaControl.DispatchEvent(string name)
        {
            switch (name)
            {
                case "dropped":
                    RaWebControl parent = Parent as RaWebControl;
                    parent.Style.SetStyleValueViewStateOnly("left", Page.Request.Params["x"] + "px");
                    parent.Style.SetStyleValueViewStateOnly("top", Page.Request.Params["y"] + "px");
                    parent.Style.SetStyleValueViewStateOnly("position", "absolute");
                    if (Dropped != null)
                        Dropped(this, new EventArgs());
				    string drops = Page.Request.Params["drops"];
				    if( !string.IsNullOrEmpty(drops) )
					{
					    string[] affectedDroppersIds = drops.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
					    foreach( string idx in affectedDroppersIds)
						{
						    BehaviorDroppable tmp = AjaxManager.Instance.FindControl<BehaviorDroppable>(idx);
					        tmp.RaiseDropped(this);
						}
					}
                    break;
                default:
                    throw new ApplicationException("Unknown event fired for control");
            }
        }
	}
}
