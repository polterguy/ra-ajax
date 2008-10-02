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

namespace Ra.Widgets
{
    /**
     * Label control, renders normally as a span, but the tag used to render the element can be overridden
     * by setting the Tag property.
     */
    [DefaultProperty("Text")]
    [ASP.ToolboxData("<{0}:Label runat=server />")]
    public class Label : RaWebControl, IRaControl
    {
        /**
         * Raised when label is clicked
         */
        public event EventHandler Click;

        /**
         * Raised when mouse is over the label, opposite of MouseOut
         */
        public event EventHandler MouseOver;

        /**
         * Raised when mouse is leaving the label, opposite of MouseOver
         */
        public event EventHandler MouseOut;

        #region [ -- Properties -- ]

        /**
         * The text that is displayed within the label, default value is string.Empty
         */
        [DefaultValue("")]
        public string Text
        {
            get { return ViewState["Text"] == null ? "" : (string)ViewState["Text"]; }
            set
            {
                if (value != Text)
                    SetJSONValueString("Text", value);
                ViewState["Text"] = value;
            }
        }

        /**
         * The HTML tag used to render the label, defaults to span
         */
        [DefaultValue("span")]
        public string Tag
        {
            get { return ViewState["Tag"] == null ? "span" : (string)ViewState["Tag"]; }
            set { ViewState["Tag"] = value; }
        }

        #endregion

        #region [ -- Overridden (abstract/virtual) methods from RaControl -- ]

        // Override this one to create specific HTML for your widgets
        protected override string GetOpeningHTML()
        {
            return string.Format("<{4} id=\"{0}\"{2}{3}>{1}",
                ClientID,
                Text,
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute(),
                Tag);
        }

        protected override string GetClosingHTML()
        {
            return string.Format("</{0}>", Tag);
        }

        #endregion

        void IRaControl.DispatchEvent(string name)
        {
            switch (name)
            {
                case "click":
                    if (Click != null)
                        Click(this, new EventArgs());
                    break;
                case "mouseover":
                    if (MouseOver != null)
                        MouseOver(this, new EventArgs());
                    break;
                case "mouseout":
                    if (MouseOut != null)
                        MouseOut(this, new EventArgs());
                    break;
                default:
                    throw new ApplicationException("Unknown event fired for control");
            }
        }

        protected override string GetEventsRegisterScript()
        {
            string evts = string.Empty;
            if (Click != null)
                evts += "['click']";
            if (MouseOver != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['mouseover']";
            }
            if (MouseOut != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['mouseout']";
            }
			return evts;
        }
	}
}
