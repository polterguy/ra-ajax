/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;
using System.Xml;
using Ra.Extensions;
using System.Collections.Generic;
using System.IO;
using ColorizerLibrary;
using Ra.Selector;
using Ra.Extensions.Widgets;
using Ra.Effects;
using Ra;
using Ra.Builder;
using Doxygen.NET;

namespace RaWebsite
{
    public partial class Docs : System.Web.UI.Page
    {
        private enum DocType
        {
	        Class,
            Tutorial
        }

        private Doxygen.NET.Docs RaDocs
        {
            get
            {
                if (Session["RaDocs"] == null)
                    Session["RaDocs"] = new Doxygen.NET.Docs(Server.MapPath("~/docs-xml"));
                return (Doxygen.NET.Docs)Session["RaDocs"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BuildRoot();
            if (!IsPostBack)
            {
                header.Text = "Reference Documentation";
                string className = Request.Params["class"];
                if (!string.IsNullOrEmpty(className))
                {
                    SelectClass(className);
                }
                else
                {
                    string tutorial = Request.Params["tutorial"];
                    if (!string.IsNullOrEmpty(tutorial))
                    {
                        tutorial = "(" + tutorial.Replace("_", " ").Replace("-", ") - ");
                        SelectTutorial(tutorial);
                    }
                }
            }
        }

        private void SelectTutorial(string tutorial)
        {
            TreeNode node = Ra.Selector.Selector.SelectFirst<TreeNode>(rootTutorials,
                delegate(System.Web.UI.Control idx)
                {
                    TreeNode n = idx as TreeNode;
                    if (n != null)
                        return n.Text == tutorial;
                    return false;
                });
            if (node != null)
            {
                rootTutorials.RollDown();
                tree.SelectedNodes = new TreeNode[] { node };
                ShowTutorial(node.ID);
            }
        }

        private void SelectClass(string className)
        {
            TreeNode node = Ra.Selector.Selector.SelectFirst<TreeNode>(rootNode,
                delegate(System.Web.UI.Control idx)
                {
                    TreeNode n = idx as TreeNode;
                    if (n != null)
                        return n.Text == className;
                    return false;
                });
            if (node != null)
            {
                root.RollDown();
                tree.SelectedNodes = new TreeNode[] { node };
                ShowClass(node.ID);
            }
        }

        private void BuildRoot()
        {
            BuildRootClasses();
            BuildRootTutorials();
        }

        private void BuildRootTutorials()
        {
            foreach (string idx in Directory.GetFiles(Server.MapPath("~/tutorials/"), "*.txt"))
            {
                if ((!string.IsNullOrEmpty(Filter) && idx.ToLower().Contains(Filter.ToLower())) || (string.IsNullOrEmpty(Filter)))
                {
                    TreeNode n = new TreeNode();
                    n.ID = "tutorial_" + idx.Replace(".txt", "").Replace(" ", "_").Replace(Server.MapPath("~/tutorials/"), "");
                    n.Text = idx.Replace(".txt", "").Replace(Server.MapPath("~/tutorials/"), "");
                    n.Xtra = idx;
                    n.CssClass = "noSample";
                    rootTutorials.Controls.Add(n);
                }
            }
        }

        private void BuildRootClasses()
        {
            List<TreeNode> l = new List<TreeNode>();
            foreach (Class idx in RaDocs.GetAllClasses())
            {
                switch (idx.FullName)
                {
                    // removing the classes we don't really NEED documentation for...
                    case "Ra.Extensions.Helpers.CometQueue":
                    case "Ra.Core.CallbackFilter":
                    case "Ra.Extensions.Widgets.AccordionView.EffectChange":
                    case "Ra.Extensions.Widgets.AutoCompleter.RetrieveAutoCompleterItemsEventArgs":
                    case "Ra.Core.PostbackFilter":
                    case "Ra.Widgets.ListItemCollection":
                    case "Ra.Widgets.StyleCollection":
                    case "Ra.Widgets.StyleCollection.StyleValue":
                    case "Ra.Widgets.ListItem":
                        break;
                    default:
                        {
                            if ((!string.IsNullOrEmpty(Filter) && idx.FullName.ToLower().Contains(Filter.ToLower())) || (string.IsNullOrEmpty(Filter)))
                            {
                                TreeNode n = new TreeNode();
                                n.ID = idx.FullName;
                                if (File.Exists(Server.MapPath("~/Docs-Controls/" + idx.FullName + ".ascx")))
                                {
                                    n.CssClass += " hasSample";
                                    n.Tooltip = "Have sample code";
                                }
                                else
                                    n.CssClass += " noSample";
                                n.Text = idx.FullName;
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
            if (itemToLookAt == "rootNode" || itemToLookAt == "tutorials")
                return;
            if (itemToLookAt.Contains("tutorial_"))
            {
                ShowTutorial(itemToLookAt);
            }
            else
            {
                ShowClass(itemToLookAt);
            }
        }

        private void LinkToDoc(DocType type, string doc)
        {
            using (HtmlBuilder builder = new HtmlBuilder())
            {
                using (Element element = builder.CreateElement("a"))
                {
                    element.AddAttribute("href", String.Format("Docs.aspx?{0}={1}", type.ToString().ToLower(), doc));
                    element.Write(header.Text);
                }
                header.Text = builder.ToString();
            }
        }

        private void ShowTutorial(string tutorialID)
        {
            bool first = pnlInfo.Style["display"] != "none";
            TreeNode node = Selector.FindControl<TreeNode>(rootTutorials, tutorialID);
            header.Text = node.Text;

            inherit.Text = "none";

            pnlInherits.Visible = true;
            pnlInfo.Style[Styles.display] = "none";
            repWrapper.Style["display"] = "none";
            pnlDescription.Visible = true;
            descTag.Text = "Content";
            using (TextReader reader = new StreamReader(node.Xtra))
            {
                CodeColorizer colorizer = ColorizerLibrary.Config.DOMConfigurator.Configure();
                description.Text = colorizer.ProcessAndHighlightText(
                    reader.ReadToEnd())
                    .Replace("%@", "<span class=\"yellow-code\">%@</span>")
                    .Replace(" %", " <span class=\"yellow-code\">%</span>");
            }
            if (first)
            {
                new EffectRollUp(pnlInfo, 500)
                    .ChainThese(
                        new EffectRollDown(pnlDescription, 500))
                    .Render();
            }
            else
            {
                new EffectRollUp(repWrapper, 100)
                    .ChainThese(new EffectRollUp(pnlDescription, 100),
                        new EffectRollUp(pnlInherits, 100),
                        new EffectRollDown(pnlDescription, 300))
                    .Render();
            }
            new EffectHighlight(header, 500)
                .Render();
            LinkToDoc(DocType.Tutorial, header.Text.Replace("(", "").Replace(") - ", "-").Replace(" ", "_"));
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
            Class classToShow = RaDocs.GetTypeByName(itemToLookAt) as Class;
            header.Text = classToShow.FullName;
            
            // Inherits from
            if (classToShow.BaseTypes.Count > 0 && !string.IsNullOrEmpty(classToShow.BaseTypes[0]))
            {
                inherit.Text = inherit.Xtra = RaDocs.GetTypeByID(classToShow.BaseTypes[0]).FullName;
            }
            else
            {
                inherit.Text = "none";
            }
            pnlInherits.Visible = true;
            pnlDescription.Visible = true;
            descTag.Text = "Description";
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
            description.Text = classToShow.Description;

            // Members, ONLY showing public members
            List<string> showOnly = new List<string>(new string[] { "function", "property", "event" });
            List<Member> publicMembers = classToShow.Members.FindAll(delegate(Member m) 
            { 
                return m.AccessModifier == "public" && showOnly.Contains(m.Kind); 
            });

            List<DocsItem> tmp = new List<DocsItem>();
            foreach (Member idx in publicMembers)
            {
                if (idx.FullName.Contains(itemToLookAt))
                {
                    DocsItem item = new DocsItem(idx.Name, idx.FullName);
                    item.Kind = "xx_" + idx.Kind;
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

            LoadSample(header.Text);
            LinkToDoc(DocType.Class, header.Text);
        }

        private void LoadSample(string className)
        {
            className = className + ".ascx";
            if (File.Exists(Server.MapPath("~/Docs-Controls/" + className)))
            {
                sampleDyn.LoadControls(className);

                string fileName = Server.MapPath("~/Docs-Controls/" + className + ".cs");
                string serverPath = Server.MapPath("~/");
                using (TextReader reader = new StreamReader(fileName))
                {
                    CodeColorizer colorizer = ColorizerLibrary.Config.DOMConfigurator.Configure();
                    codeLbl.Text = colorizer.ProcessAndHighlightText("<pre lang=\"cs\">" + reader.ReadToEnd() + "</pre>");
                }
                fileName = Server.MapPath("~/Docs-Controls/" + className);
                using (TextReader reader = new StreamReader(fileName))
                {
                    CodeColorizer colorizer = ColorizerLibrary.Config.DOMConfigurator.Configure();
                    markupLbl.Text = colorizer.ProcessAndHighlightText(
                        "<pre lang=\"xml\">" + 
                        reader.ReadToEnd() +
                        "</pre>")
                        .Replace("%@", "<span class=\"yellow-code\">%@</span>")
                        .Replace(" %", " <span class=\"yellow-code\">%</span>");
                }
                tab.Style[Styles.display] = "";
            }
            else
            {
                sampleDyn.ClearControls();
                codeLbl.Text = "no-code";
                markupLbl.Text = "no-code";
                tab.Style[Styles.display] = "none";
            }
        }

        protected void sampleDyn_Reload(object sender, Dynamic.ReloadEventArgs e)
        {
            System.Web.UI.Control ctrl = Page.LoadControl("~/Docs-Controls/" + e.Key);
            ctrl.ID = "DocsUserControl";
            sampleDyn.Controls.Add(ctrl);
        }

        protected void wnd_CreateTitleBarControls(object sender, Window.CreateTitleBarControlsEventArgs e)
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

            // Right side
            Label r = new Label();
            r.Text = "&nbsp;";
            r.CssClass = "filter-right";
            p.Controls.Add(r);

            CheckBox c = new CheckBox();
            c.Text = wnd.Movable ? "Dock" : "Float";
            c.CssClass = "pushPin";
            c.Checked = !wnd.Movable;
            c.CheckedChanged += delegate
            {
                if (c.Checked)
                {
                    wnd.Movable = false;
                    wnd.Style["width"] = wnd.Style["position"] =
                    wnd.Style["top"] = wnd.Style["right"] =
                    wnd.Style["z-index"] = string.Empty;
                    c.Text = "Float";
                }
                else
                {
                    wnd.Movable = true;
                    wnd.Style["width"] = "480px";
                    wnd.Style["position"] = "absolute";
                    wnd.Style["top"] = "5px";
                    wnd.Style["right"] = "15px";
                    wnd.Style["z-index"] = "50";
                    c.Text = "Dock";
                }
            };

            e.Caption.Controls.Add(p);
            e.Caption.Controls.Add(c);
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
            BuildRootClasses();
            root.ReRender();

            rootTutorials.Controls.Clear();
            BuildRootTutorials();
            rootTutorials.ReRender();

            if (Filter == string.Empty)
            {
                root.RollUp();
                rootTutorials.RollUp();
            }
            else
            {
                if (!root.Expanded && root.Controls.Count > 0)
                    root.RollDown();
                if (!rootTutorials.Expanded && rootTutorials.Controls.Count > 0)
                    rootTutorials.RollDown();
            }
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
            Panel pnl = Selector.SelectFirst<Panel>(btn.Parent);
            Label lbl = Selector.SelectFirst<Label>(pnl);
            if (!pnl.Visible || pnl.Style["display"] == "none")
            {
                if (lbl.Text == "")
                {
                    Class c = RaDocs.GetTypeByName(btn.Xtra.Remove(btn.Xtra.LastIndexOf("."))) as Class;
                    Member selectedMemebr = c.Members.Find(delegate(Member m)
                    {
                        return m.FullName == btn.Xtra;
                    });

                    if (selectedMemebr != null)
                        lbl.Text = selectedMemebr.Description;
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