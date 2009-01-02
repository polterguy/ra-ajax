/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;

namespace Ra.Extensions
{
    /**
     * tabcontrol widget. An alternative to Accordiong, though renders as a tabcontrol
     */
    [ASP.ToolboxData("<{0}:TabControl runat=server></{0}:TabControl>")]
    public class TabControl : Panel, ASP.INamingContainer
    {
        private Panel _topPanel;

        /**
         * Raised when ActiveTabViewIndex property is changed
         */
        public event EventHandler ActiveTabViewChanged;

        /**
         * Overridden to provide a sane default value
         */
        [DefaultValue("tab")]
        public override string CssClass
        {
            get
            {
                string retVal = base.CssClass;
                if (retVal == string.Empty)
                    retVal = "tab";
                return retVal;
            }
            set { base.CssClass = value; }
        }

        /**
         * Gets or sets the actively viewed TabView
         */
        [DefaultValue(0)]
        public int ActiveTabViewIndex
        {
            get { return ViewState["SelectedViewIndex"] == null ? 0 : (int)ViewState["SelectedViewIndex"]; }
            set
            {
                ViewState["SelectedViewIndex"] = value;
            }
        }
		
        /**
         * Returns the collection of TabViews in the control
         */
		public IEnumerable<TabView> Views
		{
			get
			{
				foreach (ASP.Control idx in Controls)
				{
					if (idx is TabView)
						yield return idx as TabView;
				}
			}
		}
		
        /**
         * Returns the active tabview
         */
		public TabView ActiveTabView
		{
			get
			{
				int idxNo = 0;
				foreach (TabView idx in Views)
				{
					if (idxNo == ActiveTabViewIndex)
						return idx;
					idxNo += 1;
				}
				return null;
			}
		}

        protected override void OnInit(EventArgs e)
        {
            EnsureChildControls();
            base.OnInit(e);
        }

        protected override void CreateChildControls()
        {
            // Creating top wrapper
            _topPanel = new Panel();
            _topPanel.ID = "top";
            _topPanel.CssClass = "tab-header";

            CreateChildTabViews();

            Controls.AddAt(0, _topPanel);
		}

        private int Count
        {
            get
            {
                int retVal = 0;
                foreach(TabView idx in Views)
                {
                    retVal += 1;
                }
                return retVal;
            }
        }

        private void CreateChildTabViews()
        {
            HTML.HtmlGenericControl ul = new HTML.HtmlGenericControl("ul");

            Label[] _tabHeaders = new Label[Count];
            int idxTabView = 0;

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
                _tabHeaders[idxTabView] = new Label();
                _tabHeaders[idxTabView].Tag = "li";

                string cssClass = "";
                if (tabView == ActiveTabViewIndex)
                    cssClass += "active ";
                if (tabView == 0)
                    cssClass += "first ";
                if (!string.IsNullOrEmpty(cssClass))
                    _tabHeaders[idxTabView].CssClass = cssClass;

                HTML.HtmlGenericControl left = new HTML.HtmlGenericControl("span");
                left.Attributes.Add("class", "left");
                left.InnerHtml = "&nbsp;";
                _tabHeaders[idxTabView].Controls.Add(left);

                HTML.HtmlGenericControl center = new HTML.HtmlGenericControl("span");
                center.Attributes.Add("class", "center");
                _tabHeaders[idxTabView].Controls.Add(center);

                LinkButton btn = new LinkButton();
                btn.Text = view.Caption;
                btn.ID = "tab_view_btn" + tabView;
                btn.Click += new EventHandler(btn_Click);
                center.Controls.Add(btn);

                // Setting controls to view
                view.ListElement = _tabHeaders[idxTabView];
                view.Button = btn;

                HTML.HtmlGenericControl right = new HTML.HtmlGenericControl("span");
                right.Attributes.Add("class", "right");
                right.InnerHtml = "&nbsp;";
                _tabHeaders[idxTabView].Controls.Add(right);

                ul.Controls.Add(_tabHeaders[idxTabView]);
                idxTabView += 1;
            }

            _topPanel.Controls.Add(ul);
        }

        protected override void OnPreRender(EventArgs e)
        {
            foreach (TabView idx in Views)
            {
                idx.Button.Text = idx.Caption;
                idx.ListElement.Visible = idx.Visible;
                if (idx.Enabled)
                {
                    idx.ListElement.CssClass = idx.ListElement.CssClass.Replace(" tab-disabled", "");
                }
                else
                {
                    if (idx.ListElement.CssClass.IndexOf(" tab-disabled") == -1)
                        idx.ListElement.CssClass += " tab-disabled";
                }
            }
            base.OnPreRender(e);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            int newIdx = Int32.Parse(btn.ID.Replace("tab_view_btn", ""));

            int idxNo = 0;
            foreach (TabView idx in Views)
            {
                if (idxNo++ == newIdx)
                {
                    if (!idx.Enabled)
                        return;
                }
            }
			this.SetActiveTabViewIndex(newIdx);
            if (ActiveTabViewChanged != null)
                ActiveTabViewChanged(this, new EventArgs());
        }

        /**
         * Programatically changes the ActiveTabView
         */
        public void SetActiveTabViewIndex(int idx)
        {
            if (idx == ActiveTabViewIndex)
                return;
            ActiveTabViewIndex = idx;
			_topPanel.Controls.Clear();
			CreateChildTabViews();
			_topPanel.ReRender();
        }
    }
}
