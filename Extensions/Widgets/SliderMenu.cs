/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace Ra.Extensions
{
    /**
     */
    [ASP.ToolboxData("<{0}:SliderMenu runat=\"server\"></{0}:SliderMenu>")]
    public class SliderMenu : Panel, ASP.INamingContainer
    {
        private Panel _bread = new Panel();

        /**
         * Raised when a MenuItem is selected by the user
         */
        public event EventHandler ItemClicked;

        /**
         * Overridden to provide a sane default value
         */
        [DefaultValue("slider-menu")]
        public override string CssClass
        {
            get
            {
                string retVal = base.CssClass;
                if (retVal == string.Empty)
                    retVal = "slider-menu";
                return retVal;
            }
            set { base.CssClass = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EnsureChildControls();
            if (ActiveLevel == null)
            {
                foreach (ASP.Control idx in Controls)
                {
                    if (idx is SliderMenuLevel)
                    {
                        SetAllChildrenNonVisible(idx);
                    }
                }
            }
        }

        internal void SetAllChildrenNonVisible(ASP.Control from)
        {
            foreach (ASP.Control idx in from.Controls)
            {
                if (idx is SliderMenuLevel)
                {
                    (idx as SliderMenuLevel).Style["display"] = "none";
                }
                SetAllChildrenNonVisible(idx);
            }
        }

        protected override void CreateChildControls()
        {
            CreateBreadCrumbWrapper();
            if (ActiveLevel != null)
            {
                UpdateBreadCrumb(AjaxManager.Instance.FindControl<SliderMenuLevel>(ActiveLevel));
            }
        }

        private void CreateBreadCrumbWrapper()
        {
            _bread.ID = "bread";
            _bread.CssClass = "bread-crumb";

            // To make sure it shows even when no path...
            WEBCTRLS.Literal lit = new WEBCTRLS.Literal();
            lit.ID = "litDummy";
            lit.Text = "&nbsp;";
            _bread.Controls.Add(lit);

            Controls.AddAt(0, _bread);
        }

        internal void RaiseItemClicked(SliderMenuItem item)
        {
            if (ItemClicked != null)
                ItemClicked(item, new EventArgs());
        }

        internal string ActiveLevel
        {
            get { return ViewState["ActiveLevel"] as string; }
            set { ViewState["ActiveLevel"] = value; }
        }

        internal void SetActiveLevel(SliderMenuLevel level)
        {
            if (level == null)
            {
                ActiveLevel = "breadCrumbHome";
            }
            else
            {
                ActiveLevel = level.ID;
            }
            _bread.Controls.Clear();
            UpdateBreadCrumb(level);
            _bread.ReRender();
        }

        private void UpdateBreadCrumb(SliderMenuLevel to)
        {
            ASP.Control idx = to == null ? null : to.Parent.Parent;
            while (true)
            {
                if (idx == null || idx is SliderMenu)
                {
                    break; // Finished
                }
                else if (idx is SliderMenuItem)
                {
                    LinkButton btn = new LinkButton();
                    foreach (ASP.Control idx2 in idx.Controls)
                    {
                        if (idx2 is SliderMenuLevel)
                            btn.ID = "BTN" + idx2.ID;
                    }
                    btn.Text = (idx as SliderMenuItem).Text;
                    btn.Click += btn_Click;
                    _bread.Controls.AddAt(0, btn);
                }
                idx = idx.Parent;
            }
            if (to != null)
            {
                LinkButton home = new LinkButton();
                home.ID = "BTNbreadGoHome";
                home.Text = "Home";
                home.Click += btn_Click;
                _bread.Controls.AddAt(0, home);
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            string idOfToBecomeActive = btn.ID.Substring(3);
            SliderMenuLevel level = AjaxManager.Instance.FindControl<SliderMenuLevel>(idOfToBecomeActive);
            SetActiveLevel(level);

            // Animating Menu levels...
            ASP.Control rootLevel = null;
            foreach (ASP.Control idx in Controls)
            {
                if (idx is SliderMenuLevel)
                {
                    rootLevel = idx;
                    break;
                }
            }
            new Ra.Extensions.SliderMenuItem.EffectRollOut(rootLevel, 800, true)
                .Render();
        }
    }
}
