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
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;

namespace Ra.Extensions.Widgets
{
    /**
     * Instance TabViews of the TabControl. These are the items a TabControl are made of.
     */
    [ASP.ToolboxData("<{0}:TabView runat=\"server\"></{0}:TabView>")]
    public class TabView : Panel
    {
        private LinkButton _btnInParent;
        private Panel _listElementInParent;

        public LinkButton Button
        {
            get { return _btnInParent; }
            set { _btnInParent = value; }
        }

        internal Panel ListElement
        {
            get { return _listElementInParent; }
            set { _listElementInParent = value; }
        }

        /**
         * Header text of tabview
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

        /**
         * Disables or Enables the Tab. Default value is true
         */
        [DefaultValue(true)]
        public bool Enabled
        {
            get { return ViewState["Enabled"] == null ? true : (bool)ViewState["Enabled"]; }
            set { ViewState["Enabled"] = value; }
        }

        /**
         * Overridden to provide a sane default value
         */
        [DefaultValue("content")]
        public override string CssClass
        {
            get
            {
                string retVal = base.CssClass;
                if (retVal == string.Empty)
                    retVal = "content";
                return retVal;
            }
            set { base.CssClass = value; }
        }

        protected override void OnPreRender(EventArgs e)
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
