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
    [ASP.ToolboxData("<{0}:Menu runat=\"server\"></{0}:Menu>")]
    public class Menu : RaWebControl, ASP.INamingContainer
    {
        public event EventHandler MenuItemSelected;

        internal void RaiseMenuItemSelected(MenuItem item)
        {
            if (MenuItemSelected != null)
                MenuItemSelected(item, new EventArgs());
        }

        protected override void OnPreRender(EventArgs e)
        {
            foreach (ASP.Control control in Controls)
            {
                if (control is MenuItems)
                {
                    MenuItems parent = control as MenuItems;
                    parent.Expanded = true;
                    foreach (ASP.Control item in parent.Controls)
                    {
                        if (item is MenuItem)
                            (item as MenuItem).CssClass += " top";
                    }
                    break;
                }
            }
            base.OnPreRender(e);
        }

        protected override string GetOpeningHTML()
        {
            return string.Format("<div id=\"{0}\"{1}{2}>",
                ClientID,
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute());
        }

        protected override string GetClosingHTML()
        {
            return "</div>";
        }
    }
}
