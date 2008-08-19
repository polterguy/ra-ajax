/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Ra Software AS thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Helpers;

namespace Ra.Widgets
{
    [DefaultProperty("Text")]
    [ASP.ToolboxData("<{0}:Image runat=server />")]
    public class Image : RaWebControl, IRaControl
    {
        #region [ -- Properties -- ]

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
        public override string GetHTML()
        {
            if (string.IsNullOrEmpty(AlternateText) || string.IsNullOrEmpty(ImageUrl))
                throw new ApplicationException("Cannot have an image without src and alt text");

            return string.Format("<img id=\"{0}\" src=\"{1}\" alt=\"{4}\"{2}{3} />",
                ClientID,
                ImageUrl,
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute(),
                AlternateText);
        }

        #endregion
    }
}
