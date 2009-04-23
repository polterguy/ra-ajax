/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;
using System.Xml;
using Ra.Extensions;
using System.Collections.Generic;

namespace RaWebsite
{
    public partial class Documentation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                header.Text = "Reference Documentation";
            }
            BuildRoot();
        }

        private void BuildRoot()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/docs-xml/index.xml"));
            List<TreeNode> l = new List<TreeNode>();
            foreach (XmlNode idx in doc.SelectNodes("/doxygenindex/compound[@kind=\"class\"]"))
            {
                switch (idx.ChildNodes[0].InnerText.Replace("::", "."))
                {
                    case "Extensions.Helpers.CometQueue":
                    case "Ra.CallbackFilter":
                    case "Ra.Extensions.AccordionView.EffectChange":
                    case "Ra.Extensions.AutoCompleter.RetrieveAutoCompleterItemsEventArgs":
                    case "Ra.PostbackFilter":
                    case "Ra.Widgets.ListItemCollection":
                    case "Ra.Widgets.StyleCollection":
                    case "Ra.Widgets.StyleCollection.StyleValue":
                    case "Ra.Widgets.ListItem":
                        break;
                    default:
                        {
                            string value = idx.ChildNodes[0].InnerText.Replace("::", ".");
                            if ((!string.IsNullOrEmpty(Filter) && value.ToLower().Contains(Filter.ToLower())) || (string.IsNullOrEmpty(Filter)))
                            {
                                TreeNode n = new TreeNode();
                                n.ID = idx.Attributes["refid"].Value;
                                n.Text = value;
                                l.Add(n);
                            }
                        } break;
                }
            }
            l.Sort(
                delegate(TreeNode left, TreeNode right)
                {
                    return left.Text.CompareTo(right.Text);
                });
            foreach (TreeNode idx in l)
            {
                root.Controls.Add(idx);
            }
        }

        protected void tree_SelectedNodeChanged(object sender, EventArgs e)
        {
            string itemToLookAt = tree.SelectedNodes[0].ID;
            ShowClass(itemToLookAt);
        }

        protected void ViewInherited(object sender, EventArgs e)
        {
            if (inherit.Text == "none")
            {
                new EffectHighlight(inherit, 500)
                    .Render();
            }
            else
            {
                ShowClass(inherit.Xtra);
            }
        }

        private void ShowClass(string itemToLookAt)
        {
            bool first = pnlInfo.Style["display"] != "none";
            string fileName = itemToLookAt + ".xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/docs-xml/" + fileName));
            string className = doc.SelectNodes("/doxygen/compounddef/compoundname")[0].InnerText;
            header.Text = className.Replace("::", ".");

            // Inherits from
            XmlNodeList lst = doc.SelectNodes("/doxygen/compounddef/basecompoundref");
            if (lst != null && lst.Count > 0)
            {
                string inheritsFrom = lst[0].InnerText;
                inherit.Text = inheritsFrom;
                inherit.Xtra = lst[0].Attributes["refid"].Value;
            }
            else
            {
                inherit.Text = "none";
            }
            pnlInherits.Visible = true;
            pnlDescription.Visible = true;
            if (first)
            {
                new EffectRollUp(pnlInfo, 500)
                    .ChainThese(
                    new EffectRollDown(pnlInherits, 500)
                        .ChainThese(new EffectRollDown(pnlDescription, 500)
                            .ChainThese(new EffectRollDown(repWrapper, 500))))
                    .Render();
                repWrapper.Style["display"] = "none";
            }
            else
            {
                new EffectRollUp(repWrapper, 100)
                    .ChainThese(new EffectRollUp(pnlDescription, 100),
                        new EffectRollUp(pnlInherits, 100),
                        new EffectRollDown(pnlInherits, 300)
                            .ChainThese(new EffectRollDown(pnlDescription, 300)
                                .ChainThese(new EffectRollDown(repWrapper, 300))))
                    .Render();
            }
            new EffectHighlight(header, 500)
                .Render();

            // Description
            string txtDescription = doc.SelectNodes("/doxygen/compounddef/detaileddescription")[0].InnerText;

            description.Text = txtDescription;

            // Properties, ONLY showing public stuff...
            lst = doc.SelectNodes("/doxygen/compounddef/listofallmembers/member[@prot=\"public\"]");
            List<DocsItem> tmp = new List<DocsItem>();
            foreach (XmlNode idx in lst)
            {
                if (idx.Attributes["refid"].Value.Contains(itemToLookAt))
                {
                    DocsItem item = new DocsItem(idx.ChildNodes[1].InnerText, idx.Attributes["refid"].Value);
                    string type = doc.SelectNodes("/doxygen/compounddef/sectiondef/memberdef[@id=\"" + idx.Attributes["refid"].Value + "\"]")[0].Attributes["kind"].Value;
                    item.Kind = "xx_" + type;
                    tmp.Add(item);
                }
            }
            tmp.Sort(
                delegate(DocsItem left, DocsItem right)
                {
                    if (right.Kind == left.Kind)
                        return left.Name.CompareTo(right.Name);
                    return right.Kind.CompareTo(left.Kind);
                });
            repProperties.DataSource = tmp;
            repProperties.DataBind();
            repWrapper.Visible = true;
            repWrapper.ReRender();
        }

        protected void wnd_CreateNavigationalButtons(object sender, Window.CreateNavigationalButtonsEvtArgs e)
        {
            // Filter panel
            Panel p = new Panel();
            p.CssClass = "filterBox";
            p.ID = "fltPnl";

            // Left side
            Label l = new Label();
            l.Text = "&nbsp;";
            l.CssClass = "filter-left";
            p.Controls.Add(l);

            // Actual textbox
            TextBox b = new TextBox();
            b.ID = "filter";
            b.Text = "Filter";
            b.CssClass = "filter";
            b.Focused += b_Focused;
            b.KeyUp += b_KeyUp;
            b.Blur += b_Blur;
            p.Controls.Add(b);

            // Left side
            Label r = new Label();
            r.Text = "&nbsp;";
            r.CssClass = "filter-right";
            p.Controls.Add(r);

            e.Caption.Controls.Add(p);
        }

        private string Filter
        {
            get { return ViewState["Filter"] as string; }
            set { ViewState["Filter"] = value; }
        }

        void b_KeyUp(object sender, EventArgs e)
        {
            root.Controls.Clear();
            Filter = (sender as TextBox).Text;
            BuildRoot();
            tree.ReRender();
        }

        void b_Blur(object sender, EventArgs e)
        {
            TextBox b = sender as TextBox;
            if (b.Text == "")
                b.Text = "Filter";
        }

        void b_Focused(object sender, EventArgs e)
        {
            TextBox b = sender as TextBox;
            if (b.Text == "Filter")
                b.Text = "";
        }

        protected void PropertyChosen(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            Panel pnl = RaSelector.Selector.SelectFirst<Panel>(btn.Parent);
            Label lbl = RaSelector.Selector.SelectFirst<Label>(pnl);
            if (!pnl.Visible || pnl.Style["display"] == "none")
            {
                if (lbl.Text == "")
                {
                    string fileName = btn.Xtra.Substring(0, btn.Xtra.LastIndexOf("_")) + ".xml";
                    XmlDocument doc = new XmlDocument();
                    doc.Load(Server.MapPath("~/docs-xml/" + fileName));
                    XmlNodeList node = doc.SelectNodes(
                        string.Format("/doxygen/compounddef/sectiondef/memberdef[@id=\"{0}\"]", btn.Xtra));
                    lbl.Text = node[0].SelectNodes("detaileddescription")[0].InnerText;
                }
                pnl.Visible = true;
                pnl.Style["display"] = "none";
                new EffectRollDown(pnl, 500)
                    .JoinThese(new EffectFadeIn())
                    .Render();
            }
            else
            {
                new EffectRollUp(pnl, 500)
                    .JoinThese(new EffectFadeOut())
                    .Render();
            }
        }
    }
}