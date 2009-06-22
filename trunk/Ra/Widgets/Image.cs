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
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Builder;

namespace Ra.Widgets
{
    /**
     * Image control. Renders an img HTML tag with the given property values.
     */
    [DefaultProperty("Text")]
    [ASP.ToolboxData("<{0}:Image runat=server />")]
    public class Image : RaWebControl
    {
        #region [ -- Properties -- ]

        /**
         * URL of image, renders as the src attribute of the control. Mandatory!
         */
        [DefaultValue("")]
        public string ImageUrl
        {
            get { return ViewState["ImageUrl"] == null ? "" : (string)ViewState["ImageUrl"]; }
            set
            {
                if (value != ImageUrl)
                    SetJSONGenericValue("src", value);
                ViewState["ImageUrl"] = value;
            }
        }

        /**
         * Alternative text, renders as the alt attribute of the control. Mandatory!
         */
        [DefaultValue("")]
        public string AlternateText
        {
            get { return ViewState["AlternateText"] == null ? "" : (string)ViewState["AlternateText"]; }
            set
            {
                if (value != AlternateText)
                    SetJSONGenericValue("alt", value);
                ViewState["AlternateText"] = value;
            }
        }

        #endregion

        #region [ -- Overridden (abstract/virtual) methods from RaControl -- ]

        protected override void RenderRaControl(HtmlBuilder builder)
        {
            using (Element el = builder.CreateElement("img"))
            {
                AddAttributes(el);
            }
        }

        protected override void AddAttributes(Element el)
        {
            if (string.IsNullOrEmpty(ImageUrl) || string.IsNullOrEmpty(AlternateText))
                throw new ApplicationException("Cannot have empty ImageUrl or AlternateText of Image");
            el.AddAttribute("src", ImageUrl);
            el.AddAttribute("alt", AlternateText);
            base.AddAttributes(el);
        }

        #endregion
    }
}
