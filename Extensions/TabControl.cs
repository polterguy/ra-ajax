/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen polterguy@gmail.com
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Helpers;
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;

namespace Ra.Extensions
{
    [ASP.ToolboxData("<{0}:TabControl runat=server></{0}:TabControl>")]
    public class TabControl : RaWebControl, IRaControl, ASP.INamingContainer
    {
        [DefaultValue(0)]
        public int ActiveTabViewIndex
        {
            get { return ViewState["SelectedViewIndex"] == null ? 0 : (int)ViewState["SelectedViewIndex"]; }
            set
            {
                ViewState["SelectedViewIndex"] = value;
            }
        }

        #region [ -- Overridden (abstract/virtual) methods from RaControl -- ]

        // Override this one to create specific HTML for your widgets
        public override string GetHTML()
        {
            return string.Format("<div id=\"{0}\"{2}{3}>{1}</div>",
                ClientID,
                GetChildControlsHTML(),
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute());
        }

        public override string GetClientSideScript()
        {
            string tmp = base.GetClientSideScript();
            tmp += GetChildrenClientSideScript(Controls);
            return tmp;
        }

        protected override string GetChildrenClientSideScript()
        {
            return GetChildrenClientSideScript(Controls);
        }

        private string GetChildrenClientSideScript(ASP.ControlCollection controls)
        {
            string retVal = "";
            foreach (ASP.Control idx in controls)
            {
                if (idx.Visible)
                {
                    if (idx is RaControl)
                    {
                        retVal += (idx as RaControl).GetClientSideScript();
                    }
                    retVal += GetChildrenClientSideScript(idx.Controls);
                }
            }
            return retVal;
        }

        private string GetChildControlsHTML()
        {
            // Must set all children to RenderHtml to get this to work...
            SetAllChildrenToRenderHtml(Controls);

            // Streaming Controls into Memory Stream and returning HTML as string...
            MemoryStream stream = new MemoryStream();
            TextWriter writer = new StreamWriter(stream);
            ASP.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(writer);

            // Render children
            RenderChildren(htmlWriter);
            htmlWriter.Flush();
            writer.Flush();

            // Return string representation of HTML
            stream.Position = 0;
            TextReader reader = new StreamReader(stream);
            string retVal = reader.ReadToEnd();
            return retVal;
        }

        private void SetAllChildrenToRenderHtml(ASP.ControlCollection controls)
        {
            foreach (ASP.Control idx in controls)
            {
                if (idx is RaControl)
                {
                    (idx as RaControl).Phase = RenderingPhase.RenderHtml;
                }
                SetAllChildrenToRenderHtml(idx.Controls);
            }
        }

        #endregion

        // IMPORTANT!!
        // When we have controls which contains "special" child controls we need
        // to make sure those controls are being RE-created in the OnLoad overridden method of
        // the Control.
        // This logic is being done in the next two methods...!
        protected override void OnLoad(EventArgs e)
        {
            EnsureChildControls();
            base.OnLoad(e);
        }

        protected override void CreateChildControls()
        {
            CreateChildTabViews();
        }

        private void CreateChildTabViews()
        {
            // Creating top wrapper
            Panel topWrapper = new Panel();
            topWrapper.ID = "top";
            topWrapper.CssClass = "tab-header";

            HTML.HtmlGenericControl ul = new HTML.HtmlGenericControl("ul");

            int litCount = 0;
            for (int idx = 0; idx < Controls.Count; idx++)
            {
                if (!(Controls[idx] is Panel) || Controls[idx].ID == "top")
                {
                    litCount += 1;
                    continue;
                }
                int tabView = idx - litCount;
                TabView view = Controls[idx] as TabView;
                HTML.HtmlGenericControl li = new HTML.HtmlGenericControl("li");

                if (tabView == ActiveTabViewIndex)
                    li.Attributes.Add("class", "active");

                HTML.HtmlGenericControl left = new HTML.HtmlGenericControl("span");
                left.Attributes.Add("class", "left");
                left.InnerHtml = "&nbsp;";
                li.Controls.Add(left);

                HTML.HtmlGenericControl center = new HTML.HtmlGenericControl("span");
                center.Attributes.Add("class", "center");
                li.Controls.Add(center);

                LinkButton btn = new LinkButton();
                btn.Text = view.Caption;
                btn.ID = "tab" + tabView;
                btn.Click += new EventHandler(btn_Click);
                center.Controls.Add(btn);

                HTML.HtmlGenericControl right = new HTML.HtmlGenericControl("span");
                right.Attributes.Add("class", "right");
                right.InnerHtml = "&nbsp;";
                li.Controls.Add(right);

                ul.Controls.Add(li);
            }

            topWrapper.Controls.Add(ul);

            Controls.AddAt(0, topWrapper);
        }

        void btn_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            int newIdx = Int32.Parse(btn.ID.Substring(3));
            ActiveTabViewIndex = newIdx;
            SignalizeReRender();
        }

        protected override void OnPreRender(EventArgs e)
        {
            int litCount = 0;
            for (int idx = 0; idx < Controls.Count; idx++)
            {
                if (!(Controls[idx] is Panel) || Controls[idx].ID == "top")
                {
                    litCount += 1;
                    continue;
                }
                Panel pnl = Controls[idx] as Panel;
                if (idx - litCount == ActiveTabViewIndex)
                    pnl.Visible = true;
                else
                    pnl.Visible = false;
            }
            base.OnPreRender(e);
        }
    }
}
