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
using System.Collections.Generic;

namespace Ra.Extensions
{
    /**
     */
    [ASP.ToolboxData("<{0}:SliderMenuLevel runat=\"server\"></{0}:SliderMenuLevel>")]
    public class SliderMenuLevel : Panel, ASP.INamingContainer
    {
        /**
         * Overridden to provide a sane default value
         */
        [DefaultValue("level")]
        public override string CssClass
        {
            get
            {
                string retVal = base.CssClass;
                if (retVal == string.Empty)
                    retVal = "level";
                return retVal;
            }
            set { base.CssClass = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            Tag = "ul";
            base.OnInit(e);
            if (this.Parent is SliderMenu)
                CssClass += " top-level";
        }
    }
}
