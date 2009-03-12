/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
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
         * The HTML tag used to render the label, defaults to div
         */
        [DefaultValue("div")]
        public string Tag
        {
            get { return ViewState["Tag"] == null ? "div" : (string)ViewState["Tag"]; }
            set { ViewState["Tag"] = value; }
        }

        /**
         * The button that will be clicked if user clicks enter (Carriage Return) within the panel 
         * somehow. E.g. by having focus in TextBox and clicking Enter...
         */
        [DefaultValue(null)]
        public string DefaultWidget
        {
            get { return ViewState["DefaultWidget"] == null ? null : (string)ViewState["DefaultWidget"]; }
            set { ViewState["DefaultWidget"] = value; }
        }

        protected override string GetOpeningHTML()
        {
            return string.Format("<{2} id=\"{0}\"{1}>",
                ClientID,
                GetWebControlAttributes(),
                Tag);
        }

        protected override string GetClientSideScriptOptions()
        {
            string retVal = "";
            if (!string.IsNullOrEmpty(DefaultWidget))
                retVal += string.Format("def_wdg:'{0}'", AjaxManager.Instance.FindControl(this, DefaultWidget).ClientID);
            if (_hasSetFocus)
                retVal += ",focus:true";
            return retVal;
        }
		
		protected override string GetClosingHTML ()
		{
			return string.Format("</{0}>", Tag);
		}

        #endregion
	}
}
