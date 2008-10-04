/*
 * Ra Ajax - An Ajax Library for Mono ++
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
    [ASP.ToolboxData("<{0}:Tree runat=\"server\"></{0}:Tree>")]
    public class Tree : RaWebControl, ASP.INamingContainer
    {
        /**
         * Raised when item is selected
         */
        public event EventHandler SelectedNodeChanged;

        public TreeNode SelectedNode
        {
            get
            {
                TreeNode item = null;
                if (ViewState["SelectedNode"] != null)
                {
                    item = AjaxManager.Instance.FindControl<TreeNode>(ViewState["SelectedNode"].ToString());
                }
                return item;
            }
            set
            {
                ViewState["SelectedNode"] = value.ID;
            }
        }

        internal void RaiseSelectedNodeChanged()
        {
            if (SelectedNodeChanged != null)
                SelectedNodeChanged(this, new EventArgs());
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
