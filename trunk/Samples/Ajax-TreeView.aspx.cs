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
            item.ID = "HTML_normal_tree";
            ASPCTRLS.LiteralControl lit = new ASPCTRLS.LiteralControl();
            lit.Text = "HTML";
            lit.ID = "HTML_normal_lit";
            item.Controls.Add(lit);
            parent.AddTreeViewItem(item);

            // Second child TreeViewItem
            item = new TreeViewItem();
            item.ID = "XHTML_tree";
            lit = new ASPCTRLS.LiteralControl();
            lit.Text = "XHTML";
            lit.ID = "XHTML_lit";
            item.Controls.Add(lit);
            parent.AddTreeViewItem(item);
        }

        protected void selected(object sender, EventArgs e)
        {
            TreeViewItem item = sender as TreeViewItem;
            pnlOutput1.Text = item.ID + " was selected";
            pnlOutput2.Text = GetTextForSelection(item.ID);
            Effect effect = new EffectHighlight(pnl, 500);
            effect.ChainThese(
                new EffectHighlight(pnlOutput1, 500),
                new EffectHighlight(pnlOutput2, 500)
                );
            effect.Render();
        }

        private string GetTextForSelection(string id)
        {
            switch (id)
            {
                case "good":
                    return "These are the really good things for developers in todays world";
                default:
                    return "I don't really have any intelligent to say about that theme...";
            }
        }
    }
}
