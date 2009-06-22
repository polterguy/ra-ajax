/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 * Authors: 
 * Thomas Hansen (thomas@ra-ajax.org)
 * Kariem Ali (kariem@ra-ajax.org)
 * 
 */

using System;
using Ra.Widgets;
using Ra.Extensions;
using Ra.Extensions.Widgets;
using Ra.Effects;

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

        protected void add_Click(object sender, EventArgs e)
        {
            addWindow.Visible = true;
            addUrl.Text = "URL of RSS Feed";
            addUrl.Select();
            addUrl.Focus();
        }

        protected void DeleteFeed(object sender, EventArgs e)
        {
            Guid id = new Guid(((sender as Button).Parent.Controls[1] as HiddenField).Value);
            RSSDatabase.Database.RemoveAll(
                delegate(RSS idx)
                {
                    return idx.Id == id;
                });
            grid.DataSource = RSSDatabase.Database;
            grid.DataBind();
            gridWrapper.ReRender();
            RSSFeeds.Controls.Clear();
            CreateTree();
            tree.ReRender();
        }

        protected void skin_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (skin.SelectedItem.Text)
            {
                case "Sapphire":
                    sapphire.Visible = true;
                    steel.Visible = false;
                    break;
                case "Steel":
                    sapphire.Visible = false;
                    steel.Visible = true;
                    break;
            }
        }

        protected void addBtn_Click(object sender, EventArgs e)
        {
            try
            {
                RSS r = new RSS(addUrl.Text);
                if (r.Items.Count == 0)
                    throw new Exception("X");
                RSSDatabase.Database.Add(r);
                grid.DataSource = RSSDatabase.Database;
                grid.DataBind();
                gridWrapper.ReRender();
                RSSFeeds.Controls.Clear();
                CreateTree();
                tree.ReRender();
                addWindow.Visible = false;
            }
            catch (Exception)
            {
                addUrl.Focus();
                addUrl.Select();
                errLbl.Text = "NOT a valid RSS 2.0 Feed";
                new EffectHighlight(errLbl, 200).Render();
            }
        }

        protected void addUrl_EscPressed(object sender, EventArgs e)
        {
            addWindow.Visible = false;
        }

        protected void resizer_Resized(object sender, ResizeHandler.ResizedEventArgs e)
        {
            int width = Math.Max(e.Width - 414, 400);
            int height = Math.Max(e.Height - 103, 200);
            int heightLeft = Math.Max(e.Height - 396, 50);
            wndRight.Style["width"] = width.ToString() + "px";
            pnlRight.Style["height"] = height.ToString() + "px";
            pnlLeft.Style["height"] = heightLeft.ToString() + "px";
        }
    }
}