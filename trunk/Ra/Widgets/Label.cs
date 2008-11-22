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

namespace Ra.Widgets
{
    /**
     * Label control, renders normally as a span, but the tag used to render the element can be overridden
     * by setting the Tag property.
     */
    [DefaultProperty("Text")]
    [ASP.ToolboxData("<{0}:Label runat=server />")]
    public class Label : RaWebControl
    {
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
         * If true then the Text property will be rendered AFTER the Child controls have bee rendered.
         * This is useful for places where you have Child Controls in the Label and you need to control
         * the rendering order of the Text versus the Child Controls order. Default value is false.
         * This property cannot be changed in Ajax Callbacks without calling ReRender on the label...
         */
        [DefaultValue(false)]
        public bool RenderChildControlsBeforeText
        {
            get { return ViewState["RenderChildControlsBeforeText"] == null ? false : (bool)ViewState["RenderChildControlsBeforeText"]; }
            set { ViewState["RenderChildControlsBeforeText"] = value; }
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
            return string.Format("<{2} id=\"{0}\"{3}>{1}",
                ClientID,
                (RenderChildControlsBeforeText ? "" : Text),
                Tag,
                GetWebControlAttributes());
        }

        protected override string GetClosingHTML()
        {
            return string.Format("{1}</{0}>", Tag, (RenderChildControlsBeforeText ? Text : ""));
        }

        #endregion
	}
}
