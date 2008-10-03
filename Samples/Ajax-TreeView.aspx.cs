/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using ASPCTRLS = System.Web.UI;
using Ra.Widgets;
using Ra.Extensions;

namespace Samples
{
    public partial class TreeView : System.Web.UI.Page
    {
        protected void good_2_GetChildItems(object sender, EventArgs e)
        {
            // Item that was expanded
            TreeViewItem parent = sender as TreeViewItem;

            // First child TreeViewItem
            TreeViewItem item = new TreeViewItem();
            item.ID = "HTML";
            ASPCTRLS.LiteralControl lit = new ASPCTRLS.LiteralControl();
            lit.Text = "HTML";
            lit.ID = "HTML_normal";
            item.Controls.Add(lit);
            parent.AddTreeViewItem(item);

            // Second child TreeViewItem
            item = new TreeViewItem();
            item.ID = "XHTML";
            lit = new ASPCTRLS.LiteralControl();
            lit.Text = "XHTML";
            lit.ID = "XHTML";
            item.Controls.Add(lit);
            parent.AddTreeViewItem(item);
        }
    }
}
