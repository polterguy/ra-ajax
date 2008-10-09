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
    [ASP.ToolboxData("<{0}:MenuItem runat=\"server\"></{0}:MenuItem>")]
    public class MenuItem : Panel, ASP.INamingContainer
    {
        protected override void OnInit(EventArgs e)
        {
            this.Click += MenuItem_Click;
            base.OnInit(e);
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
                this.ChildMenuItems.Style["display"] =
                    this.ChildMenuItems.Style["display"] == "none" ? "" : "none";
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
            
        }
                
        private void SetPropertiesForChildren()
        {
        }

        protected override string GetOpeningHTML()
        {
            return string.Format("<li id=\"{0}\"{1}{2}>",
                ClientID,
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute());
        }

        protected override string GetClosingHTML()
        {
            return "</li>";
        }

        // Must override this bugger to not break XHTML compliance on in-visible items...
        public override string GetInvisibleHTML()
        {
            return string.Format("<li id=\"{0}\" style=\"display:none;\" />", ClientID);
        }
    }
}