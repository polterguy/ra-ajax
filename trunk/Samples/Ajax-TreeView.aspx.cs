/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using ASPCTRLS = System.Web.UI;
using Ra.Widgets;
using Ra.Extensions;
using System.Drawing;

namespace Samples
{
    public partial class TreeView : System.Web.UI.Page
    {
        protected void good_2_GetChildNodes(object sender, EventArgs e)
        {
            // Item that was expanded
            TreeNodes parent = sender as TreeNodes;

            // First child TreeNode
            TreeNode item = new TreeNode();
            item.ID = "HTML_normal_tree";
            ASPCTRLS.LiteralControl lit = new ASPCTRLS.LiteralControl();
            lit.Text = "HTML";
            lit.ID = "HTML_normal_lit";
            item.Controls.Add(lit);
            parent.Controls.Add(item);

            // Second child TreeNode
            item = new TreeNode();
            item.ID = "XHTML_tree";
            lit = new ASPCTRLS.LiteralControl();
            lit.Text = "XHTML";
            lit.ID = "XHTML_lit";
            item.Controls.Add(lit);
            parent.Controls.Add(item);
        }

        protected void get_huge(object sender, EventArgs e)
        {
            for (int idx = 0; idx < 500; idx++)
            {
                TreeNode t = new TreeNode();
                t.ID = "huge_" + idx;
                System.Web.UI.LiteralControl l = new System.Web.UI.LiteralControl();
                l.Text = "Huge # " + idx;
                switch (idx)
                {
                    case 0:
                        l.Text += " - <em>when you</em>";
                        break;
                    case 1:
                        l.Text += " - <em>expanded</em>";
                        break;
                    case 2:
                        l.Text += " - <em>this huge</em>";
                        break;
                    case 3:
                        l.Text += " - <em>tree node</em>";
                        break;
                    case 4:
                        l.Text += " - <em>your entire</em>";
                        break;
                    case 5:
                        l.Text += " - <em>page will</em>";
                        break;
                    case 6:
                        l.Text += " - <em>feel</em>";
                        break;
                    case 7:
                        l.Text += " - <em>as going</em>";
                        break;
                    case 8:
                        l.Text += " - <em>through</em>";
                        break;
                    case 9:
                        l.Text += " - <em>syrup</em>";
                        break;
                    case 10:
                        l.Text += " - <em>and feel</em>";
                        break;
                    case 11:
                        l.Text += " - <em>less</em>";
                        break;
                    case 12:
                        l.Text += " - <em>responsive</em>";
                        break;
                    case 13:
                        l.Text += " - <em>this has nothing</em>";
                        break;
                    case 14:
                        l.Text += " - <em>todo with </em>";
                        break;
                    case 15:
                        l.Text += " - <em>Ra-Ajax but</em>";
                        break;
                    case 16:
                        l.Text += " - <em>rather the</em>";
                        break;
                    case 17:
                        l.Text += " - <em>sheer amount </em>";
                        break;
                    case 18:
                        l.Text += " - <em>of DOM nodes</em>";
                        break;
                    case 19:
                        l.Text += " - <em>in your page</em>";
                        break;
                    case 20:
                        l.Text += " - <em>after the</em>";
                        break;
                    case 21:
                        l.Text += " - <em>expansion.</em>";
                        break;
                }
                t.Controls.Add(l);
                huge_collection_node.Controls.Add(t);
            }
        }

        protected void lnk_Click(object sender, EventArgs e)
        {
            (sender as LinkButton).Text = "I was CLICKED! :)";
        }

        protected void selected(object sender, EventArgs e)
        {
            TreeNode item = tree.SelectedNode;
            pnlOutput1.Text = item.ID + " was selected";
            pnlOutput2.Text = GetTextForSelection(item.ID);

            new EffectHighlight(pnl, 500).ChainThese(
                new EffectHighlight(pnlOutput1, 500),
                new EffectHighlight(pnlOutput2, 500)
            ).Render();
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
