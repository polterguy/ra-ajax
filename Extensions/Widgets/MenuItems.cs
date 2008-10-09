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
        
        // Used for animating down when expanded
        internal void RollDown()
        {
            new EffectRollDown(this, 200)
                .JoinThese(new EffectFadeIn())
                .Render();
        }

        // Used for animating up when collapsed
        internal void RollUp()
        {
            new EffectRollUp(this, 200)
                .JoinThese(new EffectFadeIn())
                .Render();
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
