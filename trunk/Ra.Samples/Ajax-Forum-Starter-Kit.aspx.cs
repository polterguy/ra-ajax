/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;
using Ra.Extensions;
using RaSelector;
using Entities;
using Ra.Extensions.Widgets;
using Ra.Effects;

namespace Samples
{
    public partial class AjaxForum : System.Web.UI.Page
    {
        private int PageIndex
        {
            get { return ViewState["PageIndex"] == null ? 0 : (int)ViewState["PageIndex"]; }
            set { ViewState["PageIndex"] = value; }
        }

        private string PreviouslySelectedNode
        {
            get { return (string)ViewState["PreviouslySelectedNode"]; }
            set { ViewState["PreviouslySelectedNode"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CreateTree();
        }

        private void CreateTree()
        {
            int idxNo = 0;
            foreach (Node idx in Node.Nodes)
            {
                if (idxNo >= PageIndex && idxNo < PageIndex + 3)
                    BuildNode(rootNodes, idx);
                idxNo += 1;
            }
        }

        private void BuildNode(TreeNodes parent, Node node)
        {
            // First creating our TreeNode
            TreeNode t = new TreeNode();
            t.ID = "n" + node.ID.ToString().Replace("-", "");

            // Then creating a label to hold the HTML of our TreeView
            // (Headers, Username and Date etc)
            Label lbl = new Label();
            lbl.ID = "l" + node.ID.ToString().Replace("-", "");
            lbl.Text = string.Format(@"
<span class=""header"">{0}</span><span class=""username"">{1}</span><span class=""date"">{2}</span>
",
                node.Title, node.Username, FormatTime(node.Created));
            t.Controls.Add(lbl);

            // Creating "answer content"...
            // This is the one "animating out" when clicking a node...
            Panel pWrapper = new Panel();
            pWrapper.ID = "pp" + node.ID.ToString().Replace("-", "");
            pWrapper.CssClass = "bodyContentDiv";

            // Note that in order to have padding and such in combination with the 
            // EffectRollDown/Up we must have *two* levels of controls here since
            // otherwise the animation will "jump"... :(
            Label p = new Label();
            p.ID = "p" + node.ID.ToString().Replace("-", "");
            p.Tag = "div";
            pWrapper.Visible = false;
            pWrapper.Style["display"] = "none";
            p.Text = node.Body;
            p.CssClass = "bodyContent";

            // Adding "reply" button
            Button r = new Button();
            r.ID = "r" + node.ID.ToString().Replace("-", "");
            r.Text = "Reply...";
            r.CssClass = "replyBtn";
            r.Xtra = node.ID.ToString();
            r.Click += replyButton_Click;
            p.Controls.Add(r);

            pWrapper.Controls.Add(p);
            t.Controls.Add(pWrapper);

            // Creating the one holding the "children" of the TreeNode control
            // But ONLY if the Node actually HAVE children
            if (node.Children.Count > 0)
            {
                TreeNodes children = new TreeNodes();
                children.Expanded = true;
                children.ID = "c" + node.ID.ToString().Replace("-", "");
                t.Controls.Add(children);
                foreach (Node idx in node.Children)
                {
                    BuildNode(children, idx);
                }
            }
            parent.Controls.Add(t);
        }

        protected void previous_Click(object sender, EventArgs e)
        {
            PageIndex -= 3;
            if (PageIndex < 0)
            {
                PageIndex += 3;
                return;
            }
            rootNodes.Controls.Clear();
            CreateTree();
            tree.ReRender();
            new EffectHighlight(rootNodes, 500)
                .Render();
            PreviouslySelectedNode = null;
        }

        protected void next_Click(object sender, EventArgs e)
        {
            PageIndex += 3;
            if (PageIndex > Node.Nodes.Count)
            {
                PageIndex -= 3;
                return;
            }
            rootNodes.Controls.Clear();
            CreateTree();
            tree.ReRender();
            new EffectHighlight(rootNodes, 500)
                .Render();
            PreviouslySelectedNode = null;
        }

        protected void newPost_Click(object sender, EventArgs e)
        {
            parentPostID.Value = "";
            newPostWnd.Visible = true;
            newPostHeader.Text = "Header of post";
            newPostBody.Text = "";
            newPostHeader.Select();
            newPostHeader.Focus();
        }

        protected void newPostSave_Click(object sender, EventArgs e)
        {
            // Finding parent node (if any)
            Node parent = null;
            if (parentPostID.Value != "")
                parent = Node.Find(new Guid(parentPostID.Value));

            // Creating our new node and putting it into the nodes collection
            Node n = new Node(
                newPostHeader.Text,
                newPostBody.Text,
                DateTime.Now,
                "Default user");
            if (parent != null)
                parent.Children.Add(n); // HAS parent...
            else
                Node.Nodes.Add(n); // NO parent...

            // Moving to LAST page but ONLY if we're NOT replying...
            if (parent == null)
                PageIndex = (Node.Nodes.Count / 3) * 3;

            // re-rendering the tree control and doing some animation...
            rootNodes.Controls.Clear();
            CreateTree();
            tree.ReRender();
            new EffectHighlight(rootNodes, 500)
                .Render();

            // Making sure we have NOE selection...!
            PreviouslySelectedNode = null;

            // Making New Post window IN-Visible...!
            newPostWnd.Visible = false;
        }

        protected void EscPressed(object sender, EventArgs e)
        {
            newPostWnd.Visible = false;
        }

        void replyButton_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Node parent = Node.Find(new Guid(b.Xtra));
            parentPostID.Value = parent.ID.ToString();
            newPostWnd.Visible = true;
            newPostHeader.Text = "Header of post";
            newPostBody.Text = "";
            newPostHeader.Select();
            newPostHeader.Focus();
            newPostWnd.Visible = true;
        }

        // Creating beautiful time formatting with stuff like; "3 hours ago" etc...
        private string FormatTime(DateTime time)
        {
            TimeSpan span = DateTime.Now - time;
            if (span.TotalMinutes < 60)
                return Math.Round(span.TotalMinutes) + " minutes ago";
            if (span.TotalHours < 24)
                return Math.Round(span.TotalHours) + " hours ago";
            if (span.TotalDays < 5)
                return Math.Round(span.TotalDays) + " days ago";
            return time.ToString("dd. MMM yyyy", System.Threading.Thread.CurrentThread.CurrentUICulture);
        }

        protected void selected(object sender, EventArgs e)
        {
            // Finding the ID of the selected Node
            string idOfNode = tree.SelectedNodes[0].ID.Substring(1);

            // Finding the "wrapper panel" for that post
            // and animating it into view...
            Panel p = Selector.FindControl<Panel>(tree, "pp" + idOfNode);
            p.Visible = true;
            if (PreviouslySelectedNode == null)
            {
                // User had NO previous selections...
                new EffectRollDown(p, 400)
                    .Render();
                PreviouslySelectedNode = p.ID;
            }
            else
            {
                if (PreviouslySelectedNode == "pp" + idOfNode)
                {
                    // User had previously selected the SAME node
                    // therefore just "animating it away"...
                    new EffectRollUp(p, 400)
                        .Render();
                    PreviouslySelectedNode = null;
                }
                else
                {
                    // User had selected another node previously and we need
                    // to animate that node AWAY
                    // To make some "bling" we chain the animations so that the previously 
                    // selected one rolls UP *BEFORE* the one selected now
                    // rolls down...
                    new EffectRollUp(Selector.FindControl<Panel>(tree, PreviouslySelectedNode), 400)
                        .ChainThese(new EffectRollDown(p, 400))
                        .Render();
                    PreviouslySelectedNode = p.ID;
                }
            }
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