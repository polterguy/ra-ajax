﻿/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;
using Ra.Extensions;

namespace Samples
{
    public partial class RSSStarterKit : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            if (!IsPostBack)
            {
                // Binding grid...
                grid.DataSource = RSSDatabase.Database;
                grid.DataBind();
            }

            // Creating TreeNodes
            // Note this is done EVERY time...!!
            CreateTree();
            base.OnInit(e);
        }

        private void CreateTree()
        {
            foreach (RSS idxRss in RSSDatabase.Database)
            {
                // Creating feed
                TreeNode t = new TreeNode();
                t.Text = idxRss.Title;
                t.ID = idxRss.Id.ToString();
                TreeNodes nodes = new TreeNodes();
                nodes.ID = idxRss.Id.ToString() + "nodes";
                t.Controls.Add(nodes);

                // Creating items for feed
                foreach (RSSItem idxItem in idxRss.Items)
                {
                    TreeNode i = new TreeNode();
                    i.ID = idxItem.Id.ToString();
                    string header = idxItem.Header;
                    if (header.Length > 35)
                        header = header.Substring(0, 35) + " [...]";
                    i.Text = header;
                    nodes.Controls.Add(i);
                }

                // Waiting as long as possible to "root" nodes to conserve ViewState
                RSSFeeds.Controls.Add(t);
            }
        }

        protected void tree_SelectedNodeChanged(object sender, EventArgs e)
        {
            string id = tree.SelectedNodes[0].ID;
            RSSItem item = RSSDatabase.FindItem(new Guid(id));

            // It will return "null" if you click a root node...
            if (item != null)
            {
                intro.Visible = false;
                header.Text = item.Header;
                date.Text = item.Date.ToString("dddd dd. MMMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
                body.Text = item.Body;
                link.Text = string.Format(@"<a href=""{0}"">Read more...</a>", item.Url);
                new EffectHighlight(header, 300)
                    .ChainThese(new EffectHighlight(date, 300)
                        .ChainThese(new EffectHighlight(body, 300)))
                    .Render();
            }
            else
            {
                // Clicking a ROOT node...
                // Collapsing all other root nodes but the one clicked...
                foreach (TreeNode idx in RSSFeeds.Controls)
                {
                    if (idx.ID != id && idx.ChildTreeNodes != null && idx.ChildTreeNodes.Expanded)
                    {
                        idx.ChildTreeNodes.RollUp();
                    }
                }
            }
        }

        protected void resizer_Resized(object sender, ResizeHandler.ResizedEventArgs e)
        {
            int width = Math.Max(e.Width - 414, 400);
            int height = Math.Max(e.Height - 103, 200);
            int heightLeft = Math.Max(e.Height - 362, 50);
            wndRight.Style["width"] = width.ToString() + "px";
            pnlRight.Style["height"] = height.ToString() + "px";
            pnlLeft.Style["height"] = heightLeft.ToString() + "px";
        }
    }
}