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
     * A single MenuItem within a MenuItems collection which again should be within a Menu Widget.
     */
    [ASP.ToolboxData("<{0}:MenuItem runat=\"server\"></{0}:MenuItem>")]
    public class MenuItem : Label, ASP.INamingContainer
    {
        protected override void OnInit(EventArgs e)
        {
            this.Tag = "li";
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Click += MenuItem_Click;
            if (ChildMenuItems != null && ChildMenuItems.Controls.Count > 0)
            {
                if (ParentMenu.ExpandMethod == Menu.HowToExpand.Click)
                {
                    this.Click += MenuItem_Expand;
                    this.MouseOut += MenuItem_MouseOut;
                }
                else
                {
                    this.MouseOver += MenuItem_Expand;
                    this.MouseOut += MenuItem_MouseOut;
                }
            }
        }

        void MenuItem_MouseOut(object sender, EventArgs e)
        {
            if (ChildMenuItems != null)
            {
                if (ChildMenuItems.Expanded)
                {
                    ChildMenuItems.RollUp();
                    ChildMenuItems.Expanded = false;
                }
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

        private MenuItems ChildMenuItems
        {
            get
            {
                foreach (ASP.Control idx in Controls)
                {
                    if (idx is MenuItems)
                        return idx as MenuItems;
                }
                return null;
            }
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            ParentMenu.RaiseMenuItemSelected(this);
            if (ChildMenuItems != null)
            {
                if (!ChildMenuItems.Expanded)
                {
                    ChildMenuItems.RollDown();
                    ChildMenuItems.Expanded = true;
                }
            }
            else
            {
                ASP.Control parent = Parent;
                while (parent != null && !(parent is Menu))
                {
                    if (parent is MenuItems && !(parent.Parent is Menu))
                    {
                        (parent as MenuItems).RollUp();
                        (parent as MenuItems).Expanded = false;
                    }
                    parent = parent.Parent;
                }
            }
        }

        private void MenuItem_Expand(object sender, EventArgs e)
        {
            if (ChildMenuItems != null)
            {
                if (!ChildMenuItems.Expanded)
                {
                    ChildMenuItems.RollDown();
                    ChildMenuItems.Expanded = true;
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            BuildCss();
            base.OnPreRender(e);
        }

        private void BuildCss()
        {
            BuildCssForRootElement();
            SetPropertiesForChildren();
        }

        // Build CSS classes for the "root" DOM element ("this control")
        private void BuildCssForRootElement()
        {
            if (!CssClass.Contains("item"))
                CssClass += " item";
            if (ChildMenuItems != null && 
                ChildMenuItems.Controls.Count > 0 && 
                !CssClass.Contains("has-children") && 
                !CssClass.Contains("top"))
                CssClass += " has-children";
        }
                
        private void SetPropertiesForChildren()
        {
            foreach (ASP.Control control in Controls)
            {
                if (control is MenuItems)
                {
                    MenuItems parent = control as MenuItems;
                    if (parent.CssClass.IndexOf(" menu-dropper") == -1)
                        parent.CssClass += " menu-dropper";

                    foreach (ASP.Control item in parent.Controls)
                    {
                        if (item is MenuItem)
                        {
                            MenuItem menuItem = item as MenuItem;
                            if (menuItem.CssClass.IndexOf(" drop-item") == -1)
                                menuItem.CssClass += " drop-item";
                        }
                    }
                    break;
                }
            }
        }
    }
}
