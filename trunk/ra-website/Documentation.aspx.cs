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
                        TreeNode n = new TreeNode();
                        n.ID = idx.Attributes["refid"].Value;
                        n.Text = idx.ChildNodes[0].InnerText.Replace("::", ".");
                        l.Add(n);
                        break;
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
            bool first = pnlInfo.Style["display"] != "none";
            string fileName = tree.SelectedNodes[0].ID + ".xml";
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
            foreach( XmlNode idx in lst)
            {
                tmp.Add(new DocsItem(idx.ChildNodes[1].InnerText, idx.Attributes["refid"].Value));
            }
            repProperties.DataSource = tmp;
            repProperties.DataBind();
            repWrapper.ReRender();
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
                    .Render();
            }
            else
            {
                new EffectRollUp(pnl, 500)
                    .Render();
            }
        }
    }
}