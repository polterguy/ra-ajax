/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Helpers;
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;

namespace Ra.Extensions
{
    [ASP.ToolboxData("<{0}:TabView runat=server></{0}:TabView>")]
    public class TabView : Panel
    {
        [DefaultValue("")]
        public string Caption
        {
            get { return ViewState["Caption"] == null ? "" : (string)ViewState["Caption"]; }
            set
            {
                ViewState["Caption"] = value;
            }
        }
    }
}
