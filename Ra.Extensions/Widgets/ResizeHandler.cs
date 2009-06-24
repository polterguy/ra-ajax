/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
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
using Ra.Builder;

[assembly: ASP.WebResource("Ra.Extensions.Js.ResizeHandler.js", "text/javascript")]

namespace Ra.Extensions.Widgets
{
    /**
     * A ResizeHandler widget is a special (non-visual) control which will raise an event when
     * the browser window is resized. Very useful for creating "portal websites" which needs to
     * know the exact size of the browser window at all times.
     */
    [ASP.ToolboxData("<{0}:ResizeHandler runat=\"server\" />")]
    public class ResizeHandler : RaControl, IRaControl
    {
        /**
         * EventArgs passed when Viewport is resized. Contains information about the Viewport (browser)
         * width and height.
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
            AjaxManager.Instance.IncludeScriptFromResource(typeof(Timer), "Ra.Extensions.Js.ResizeHandler.js");
        }

        public int Width
        {
            get { return (int)ViewState["_width"]; }
            private set { ViewState["_width"] = value; }
        }

        public int Height
        {
            get { return (int)ViewState["_height"]; }
            private set { ViewState["_height"] = value; }
        }

        void IRaControl.DispatchEvent(string name)
        {
            switch (name)
            {
                case "resized":
                    if (Resized != null)
                    {
                        int width = Int32.Parse(Page.Request.Params["width"]);
                        int height = Int32.Parse(Page.Request.Params["height"]);
                        Width = width;
                        Height = height;
                        Resized(this,
                            new ResizedEventArgs(
                                width,
                                height));
                    }
                    break;
            }
        }

		protected override string GetClientSideScriptType()
		{
			return "new Ra.ResHand";
		}

        protected override void RenderRaControl(HtmlBuilder builder)
        {
            using (Element el = builder.CreateElement("span"))
            {
                AddAttributes(el);
                el.Write("&nbsp;");
            }
        }

        protected override void AddAttributes(Element el)
        {
            el.AddAttribute("style", "display:none;");
            base.AddAttributes(el);
        }
    }
}
