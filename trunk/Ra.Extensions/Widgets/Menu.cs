/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
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

namespace Ra.Extensions.Widgets
{
    /**
     * A Menu Widget for creating Menus on your websites and applications. Use in combination with
     * the MenuItems and the MenuItem widgets. The theory is that a Menu consists of one or several
     * MenuItems which again consists of one of several MenuItem (singular form) which again can contain
     * MenuItems again. And so it goes recursively.
     */
    [ASP.ToolboxData("<{0}:Menu runat=\"server\"></{0}:Menu>")]
    public class Menu : RaWebControl, ASP.INamingContainer
    {
        public event EventHandler MenuItemSelected;

        /**
         * Overridden to provide a sane default value
         */
        [DefaultValue("menu")]
        public override string CssClass
        {
            get
            {
                string retVal = base.CssClass;
                if (retVal == string.Empty)
                    retVal = "menu";
                return retVal;
            }
            set { base.CssClass = value; }
        }

        internal void RaiseMenuItemSelected(MenuItem item)
        {
            if (MenuItemSelected != null)
                MenuItemSelected(item, new EventArgs());
        }

        protected override void OnInit(EventArgs e)
        {
            Label left = new Label();
            left.ID = "leftSpan";
            left.Text = "&nbsp;";
            left.CssClass = "top-left";

            Label right = new Label();
            right.ID = "rightSpan";
            right.Text = "&nbsp;";
            right.CssClass = "top-right";

            Controls.AddAt(0, left);

            Controls.Add(right);

            base.OnInit(e);
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
            return string.Format("<div id=\"{0}\"{1}>",
                ClientID,
                GetWebControlAttributes());
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
