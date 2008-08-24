/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Ra Software AS thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
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
    public class TabControl : Panel, ASP.INamingContainer
    {
        public event EventHandler ActiveTabViewChanged;

        [DefaultValue(0)]
        public int ActiveTabViewIndex
        {
            get { return ViewState["SelectedViewIndex"] == null ? 0 : (int)ViewState["SelectedViewIndex"]; }
            set
            {
                ViewState["SelectedViewIndex"] = value;
            }
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            // Since we're dependant upon that the ViewState has finished loading before
            // we initialize the ChildControls since how the child controls (and which)
            // child controls are being re-created is dependant upon a ViewState saved value
            // this is the earliest possible time we can reload the ChildControls for the
            // Control
            EnsureChildControls();
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

                string cssClass = "";
                if (tabView == ActiveTabViewIndex)
                    cssClass += "active ";
                if (tabView == 0)
                    cssClass += "first ";
                if (!string.IsNullOrEmpty(cssClass))
                    li.Attributes.Add("class", cssClass);

                HTML.HtmlGenericControl left = new HTML.HtmlGenericControl("span");
                left.Attributes.Add("class", "left");
                left.InnerHtml = "&nbsp;";
                li.Controls.Add(left);

                HTML.HtmlGenericControl center = new HTML.HtmlGenericControl("span");
                center.Attributes.Add("class", "center");
                li.Controls.Add(center);

                LinkButton btn = new LinkButton();
                btn.Text = view.Caption;
                btn.ID = "tab_view_btn" + tabView;
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
            int newIdx = Int32.Parse(btn.ID.Replace("tab_view_btn", ""));
            ActiveTabViewIndex = newIdx;
            ReRender();
            Controls.RemoveAt(0);
            CreateChildTabViews();
            if (ActiveTabViewChanged != null)
                ActiveTabViewChanged(this, new EventArgs());
        }

        public void SetActiveTabViewIndex(int idx)
        {
            if (idx == ActiveTabViewIndex)
                return;
            ActiveTabViewIndex = idx;
            ReRender();
            Controls.RemoveAt(0);
            CreateChildTabViews();
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
