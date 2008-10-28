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
using System.Collections.Generic;

namespace Ra.Extensions
{
    [ASP.ToolboxData("<{0}:MenuItems runat=\"server\"></{0}:MenuItems>")]
    public class MenuItems : RaWebControl, ASP.INamingContainer
    {
        [DefaultValue(false)]
        internal bool Expanded
        {
            get { return ViewState["Expanded"] == null ? false : (bool)ViewState["Expanded"]; }
            set { ViewState["Expanded"] = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!Expanded)
                Style["display"] = "none";

            if (Level >= 2)
            {
                if (CssClass.IndexOf("sub-dropper") == -1)
                    CssClass += " sub-dropper";
            }
                      
            base.OnPreRender(e);
        }

        private int Level
        {
            get 
            {
                int level = 0;
                ASP.Control ctrl = this.Parent;
                while (ctrl != null && !(ctrl is Menu))
                {
                    if (ctrl is MenuItems) 
                        level++;
                    ctrl = ctrl.Parent;
                }
                return level;
            }
        }

        private Menu ParentMenu
        {
            get
            {
                // Looping upwards in the Control hierarchy until we 
                // find a Control of type "Menu"
                ASP.Control ctrl = this.Parent;
                while (ctrl != null && !(ctrl is Menu))
                    ctrl = ctrl.Parent;
                return ctrl as Menu;
            }
        }

        // Used for animating down when expanded
        internal void RollDown()
        {
            if (Style["display"] == "none")
            {
                ParentMenu.RollUpAllExceptThis(this, ParentMenu.Controls);
                if (Page.Request.Browser.Browser == "IE")
                    Style["display"] = "";
                else
                    new EffectFadeIn(this, 300)
                        .Render();
            }
        }

        // Used for animating up when collapsed
        internal void RollUp()
        {
            if (Style["display"] != "none")
            {
                if (Page.Request.Browser.Browser == "IE")
                    Style["display"] = "none";
                else
                    new EffectFadeOut(this, 300)
                        .Render();
            }
        }

        protected override string GetOpeningHTML()
        {
            return string.Format("<ul id=\"{0}\"{1}{2}>",
                ClientID,
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute());
        }

        protected override string GetClosingHTML()
        {
            return "</ul>";
        }
    }
}
