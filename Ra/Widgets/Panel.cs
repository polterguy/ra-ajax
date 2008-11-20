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
using System.IO;

namespace Ra.Widgets
{
    /**
     * Panel control, renders as &lt;div...
     */
    [DefaultProperty("CssClass")]
    [ASP.ToolboxData("<{0}:Panel runat=server></{0}:Panel>")]
    public class Panel : RaWebControl, ASP.INamingContainer
    {
        #region [ -- Overridden (abstract/virtual) methods from RaControl -- ]

        /**
         * The HTML tag used to render the label, defaults to span
         */
        [DefaultValue("div")]
        public string Tag
        {
            get { return ViewState["Tag"] == null ? "div" : (string)ViewState["Tag"]; }
            set { ViewState["Tag"] = value; }
        }

        protected override string GetOpeningHTML()
        {
            return string.Format("<{2} id=\"{0}\"{1}>",
                ClientID,
                GetWebControlAttributes(),
                Tag);
        }
		
		protected override string GetClosingHTML ()
		{
			return string.Format("</{0}>", Tag);
		}

        #endregion
	}
}
