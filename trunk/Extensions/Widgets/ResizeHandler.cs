/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;

[assembly: ASP.WebResource("Extensions.Js.ResizeHandler.js", "text/javascript")]

namespace Ra.Extensions
{
    /**
     * Ajax timer, raises Tick evnt handler back to server periodically. Alternative to Comet component and
     * far "safer" to use than Comet
     */
    [ASP.ToolboxData("<{0}:ResizeHandler runat=\"server\" />")]
    public class ResizeHandler : RaControl, IRaControl
    {
        /**
         * EventArgs passed when Viewport is resized
         */
        public class ResizedEventArgs : EventArgs
        {
            private int _width;
            private int _height;

            internal ResizedEventArgs(int width, int height)
            {
                _width = width;
                _height = height;
            }

            public int Width
            {
                get { return _width; }
            }

            public int Height
            {
                get { return _height; }
            }
        }

        /**
         * Raised periodically every Duration milliseconds
         */
        public event EventHandler<ResizedEventArgs> Resized;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            AjaxManager.Instance.IncludeScriptFromResource(typeof(Timer), "Extensions.Js.ResizeHandler.js");
        }

        void IRaControl.DispatchEvent(string name)
        {
            switch (name)
            {
                case "resized":
                    if (Resized != null)
                        Resized(this, 
                            new ResizedEventArgs(
                                Int32.Parse(Page.Request.Params["width"]), 
                                Int32.Parse(Page.Request.Params["height"])));
                    break;
            }
        }

		protected override string GetClientSideScriptType()
		{
			return "new Ra.ResHand";
		}

        protected override string GetOpeningHTML()
        {
            // Dummy HTML DOM element to make registration and such easier...
            return string.Format("<input type=\"hidden\" id=\"{0}\" />", ClientID);
        }
    }
}
