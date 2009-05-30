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

        // Override this one to create specific HTML for your widgets
        protected override string GetOpeningHTML()
        {
            if (string.IsNullOrEmpty(AlternateText) || string.IsNullOrEmpty(ImageUrl))
                throw new ApplicationException("Cannot have an image without src and alt text");

            return string.Format("<img id=\"{0}\" src=\"{1}\" alt=\"{2}\"{3} />",
                ClientID,
                ImageUrl,
                AlternateText,
                GetWebControlAttributes());
        }

        #endregion
    }
}
