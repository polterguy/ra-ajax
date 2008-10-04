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
    [ASP.ToolboxData("<{0}:TreeNodes runat=\"server\"></{0}:TreeNodes>")]
    public class TreeNodes : RaWebControl, ASP.INamingContainer
    {
        [Browsable(false)]
        public IEnumerable<TreeNode> Items
        {
            get
            {
                foreach (ASP.Control idx in Controls)
                {
                    if (idx is TreeNode)
                        yield return idx as TreeNode;
                }
            }
        }

        public TreeNode SelectedItem
        {
            get
            {
                TreeNode item = null;
                if (ViewState["SelectedItem"] != null)
                {
                    item = AjaxManager.Instance.FindControl<TreeNode>(ViewState["SelectedItem"].ToString());
                }
                return item;
            }
            set
            {
                ViewState["SelectedItem"] = value.ID;
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
