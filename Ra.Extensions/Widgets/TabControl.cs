/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
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

namespace Ra.Extensions.Widgets
{
    /**
     * TabControl widget. An alternative to Accordion. Contains several sheets in horizontal layout
     * which can be clicked to change the currently "active view". Useful for grouping information on
     * screen into different categories. A TabControl is a collection of TabView items.
     */
    [ASP.ToolboxData("<{0}:TabControl runat=server></{0}:TabControl>")]
    public class TabControl : Panel, ASP.INamingContainer
    {
        public class ActiveTabViewClosedEventArgs : EventArgs
        {
            internal ActiveTabViewClosedEventArgs(int tabViewIndex)
            {
                TabViewIndex = tabViewIndex;
            }

            public int TabViewIndex { get; set; }
        }

        private Panel _topPanel;

        /**
         * Raised when ActiveTabViewIndex property is changed
         */
        public event EventHandler ActiveTabViewChanged;

        /**
         * Raised when TabView is being closed
         */
        public event EventHandler<ActiveTabViewClosedEventArgs> ActiveTabViewClosed;

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
            set { ViewState["SelectedViewIndex"] = value; }
        }
		
        /**
         * Returns the collection of TabViews in the control
         */
		public List<TabView> Views
		{
			get
			{
                List<TabView> views = new List<TabView>();
				foreach (ASP.Control idx in Controls)
				{
                    if (idx is TabView)
                        views.Add(idx as TabView);
				}
                return views;
			}
		}

        public List<TabView> EnabledVisibleViews
        {
            get
            {
                return Views.FindAll(
                    delegate(TabView tab)
                    {
                        return tab.Enabled && tab.Visible;
                    });
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

        /**
         * If true then the changing of Active view will happen purely on the client and
         * not create an Ajax Request at all. Warning! If you chose to set this property
         * to true, then the control will not raise the ActiveViewChanged event
         * when active view is changed!
         * Also this value must be set when control is created or shown for the first time
         * and cannot be changed after control is created.
         */
        [DefaultValue(false)]
        public bool ClientSideChange
        {
            get
            {
                if (ViewState["ClientSideChange"] == null)
                    return false;
                return (bool)ViewState["ClientSideChange"];
            }
            set
            {
                ViewState["ClientSideChange"] = value;
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

        private void CreateChildTabViews()
        {
            HTML.HtmlGenericControl ul = new HTML.HtmlGenericControl("ul");

            Panel[] _tabHeaders = new Panel[Views.Count];
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
                _tabHeaders[idxTabView] = new Panel();
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
                center.Controls.Add(btn);

                // Setting controls to view
                view.ListElement = _tabHeaders[idxTabView];
                view.Button = btn;

                HTML.HtmlGenericControl right = new HTML.HtmlGenericControl("span");
                right.Attributes.Add("class", "right");
                right.InnerHtml = "&nbsp;";
                _tabHeaders[idxTabView].Controls.Add(right);

                LinkButton closeButton = new LinkButton();
                closeButton.ID = "tab_view_xbtn" + tabView;
                closeButton.Text = "&nbsp;";
                right.Controls.Add(closeButton);
                view.CloseButton = closeButton;

                ul.Controls.Add(_tabHeaders[idxTabView]);
                idxTabView += 1;
            }

            _topPanel.Controls.Add(ul);

            foreach (TabView idx in Views)
            {
                if (ClientSideChange)
                {
                    string exTabs = "";
                    string exTabsBody = "";
                    foreach (TabView idxView in Views)
                    {
                        if (!string.IsNullOrEmpty(exTabs))
                            exTabs += ",";
                        exTabs += "'" + idxView.ListElement.ClientID + "'";
                        if (!string.IsNullOrEmpty(exTabsBody))
                            exTabsBody += ",";
                        exTabsBody += "'" + idxView.ClientID + "'";
                    }
                    string func = string.Format(@"function() {{
var tabs = [{0}];
var tabsBody = [{2}];
var oldSelBtn;
var oldSelTab;
for( var idx=0; idx < tabs.length; idx++ ) {{
  var el = Ra.$(tabs[idx]);
  if( el.className.indexOf('active') != -1 ) {{
    oldSelBtn = el;
    oldSelTab = Ra.$(tabsBody[idx]);
  }}
}}
if( Ra.$('{1}').className.indexOf('tab-disabled') != -1 )
  return;
oldSelBtn.removeClassName('active');
Ra.$('{1}').addClassName('active');
oldSelTab.style.display = 'none';
Ra.$('{3}').style.display = '';
}}", exTabs, idx.ListElement.ClientID, exTabsBody, idx.ClientID);
                    idx.Button.OnClickClientSide = func;
                }
                else
                {
                    idx.Button.Click += btn_Click;
                    idx.CloseButton.Click += closeButton_Click;
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            bool enabledClosedButtons = EnabledVisibleViews.Count >= 2;
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
                idx.CloseButton.Visible = enabledClosedButtons;
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

        private void closeButton_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            int newIdx = Int32.Parse(btn.ID.Replace("tab_view_xbtn", ""));

            if (ActiveTabViewClosed != null)
            {
                ActiveTabViewClosed(this, new ActiveTabViewClosedEventArgs(newIdx));
            }

            Views[newIdx].Visible = false;
            
            if (ActiveTabViewIndex == newIdx)
                SetActiveTabViewIndex(Views.FindIndex(delegate(TabView tab) { return tab.Visible && tab.Enabled; }));

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
