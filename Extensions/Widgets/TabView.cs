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

namespace Ra.Extensions
{
    /**
     * Instance tabviews of the TabControl
     */
    [ASP.ToolboxData("<{0}:TabView runat=\"server\"></{0}:TabView>")]
    public class TabView : Panel
    {
        /**
         * text header of tabview
         */
        [DefaultValue("")]
        public string Caption
        {
            get { return ViewState["Caption"] == null ? "" : (string)ViewState["Caption"]; }
            set
            {
                ViewState["Caption"] = value;
            }
        }
		
		protected override void OnPreRender (EventArgs e)
		{
			if ((Parent as TabControl).ActiveTabView.ClientID == this.ClientID)
			{
				Style["display"] = "block";
			}
			else
			{
				Style["display"] = "none";
			}
			base.OnPreRender (e);
		}
    }
}
