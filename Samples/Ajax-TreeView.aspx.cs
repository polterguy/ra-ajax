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
        protected void good_2_GetChildItems(object sender, TreeViewItem.GetChildItemsEventArgs e)
        {
            // Item that was expanded
            TreeViewItem parent = sender as TreeViewItem;

            // First child TreeViewItem
            TreeViewItem item = new TreeViewItem();
            item.ID = "HTML_normal_tree";
            item.Selected += selected;
            ASPCTRLS.LiteralControl lit = new ASPCTRLS.LiteralControl();
            lit.Text = "HTML";
            lit.ID = "HTML_normal_lit";
            item.Controls.Add(lit);
            e.Children.Add(item);

            // Second child TreeViewItem
            item = new TreeViewItem();
            item.ID = "XHTML_tree";
            item.Selected += selected;
            lit = new ASPCTRLS.LiteralControl();
            lit.Text = "XHTML";
            lit.ID = "XHTML_lit";
            item.Controls.Add(lit);
            e.Children.Add(item);
        }

        protected void selected(object sender, EventArgs e)
        {
            TreeViewItem item = sender as TreeViewItem;
            pnlOutput1.Text = item.ID + " was selected";
            pnlOutput2.Text = GetTextForSelection(item.ID);
            Effect effect = new EffectFadeIn(pnl, 500);
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
                case "ajax":
                    return "The 3rd IT revolution";
                case "jQuery":
                    return "Great low-level Ajax Framework for skilled JavaScript developers";
                case "prototype":
                    return "Together with Dojo the first 'real' JavaScript library out there";
                case "mooTools":
                    return "Probably one of the BEST JavaScript libraries out there, EXTREMELY small, lightweight and slick (for being a JavaScript library)";
                case "html":
                    return "Basically the foundation of the 'second' IT revolution with the web as the communication channel";
                case "css":
                    return "Brilliant way to separate looks and content by our friends from Opera";
                case "why_cool":
                    return "Just proof of that you can combine controls with TreeViewItems";
                case "why_cool2":
                    return "Another proof";
                case "bad":
                    return "Evil attempts to try to shut down the Open Web";
                case "flex":
                    return "Adobe's attempt at trying to 'inherit' the dead king";
                case "silverlight":
                    return "Microsoft's attempt at trying to get 'another 30 years' with the throne";
                case "activex":
                    return "Binary BLOB technology for making 'rich internet applications'";
                case "activex1":
                    return "First attempt, we all agree on that this is bad today";
                case "activex2":
                    return "Collection of 'new' ActiveX technologies, includes Flex and Silverlight";
                case "HTML_normal_tree":
                    return "First attempt, lacks fundamental qualities needed in a document format";
                case "XHTML_tree":
                    return "Second attempt, contains guarantees and stricter validation that makes it a 'better fit' than HTML";
                default:
                    return "I don't really have any intelligent to say about that theme...";
            }
        }
    }
}
