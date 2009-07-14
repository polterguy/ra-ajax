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
using Ra.Builder;

namespace Ra.Widgets
{
    /**
     * Label control, renders normally as a span, but the tag used to render the element can be overridden
     * by setting the Tag property. This is the "display some text control" of Ra-Ajax.
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

        protected override void RenderRaControl(HtmlBuilder builder)
        {
            using (Element el = builder.CreateElement(Tag))
            {
                AddAttributes(el);
                el.Write(Text);
                // TODO: Either have Text OR Children, or...?
                RenderChildren(builder.Writer as System.Web.UI.HtmlTextWriter);
            }
        }

        protected override void AddAttributes(Element el)
        {
            base.AddAttributes(el);
        }

        #endregion
	}
}
