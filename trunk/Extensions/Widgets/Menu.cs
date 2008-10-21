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
                        {
                            MenuItem menuItem = item as MenuItem;
                            if (menuItem.CssClass.IndexOf(" top") == -1)
                                menuItem.CssClass += " top";
                        }
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

        internal void RollUpAllExceptThis(MenuItems menuItems, ASP.ControlCollection cols)
        {
            foreach (ASP.Control idx in cols)
            {
                if (idx is MenuItems)
                {
                    MenuItems items = idx as MenuItems;
                    if (!items.Expanded)
                        continue;
                    bool isParent = false;
                    ASP.Control idxCtrl = menuItems;
                    while (idxCtrl != null && !(idxCtrl is Menu))
                    {
                        if (idxCtrl == items)
                        {
                            isParent = true;
                            break;
                        }
                        idxCtrl = idxCtrl.Parent;
                    }
                    if (!isParent)
                    {
                        items.Expanded = false;
                        items.RollUp();
                    }
                    else
                    {
                        foreach (ASP.Control idxInner in idx.Controls)
                        {
                            RollUpAllExceptThis(menuItems, idxInner.Controls);
                        }
                    }
                }
            }
        }
    }
}
