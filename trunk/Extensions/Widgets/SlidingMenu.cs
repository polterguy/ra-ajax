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
     * A SliderMenu is maybe easiest to define as a combination of a Tree and a Menu. Though while the
     * Tree can display every active and opened nodes at the same time, the SliderMenu can only display
     * one "level" at the time. Then when you select SliderMenuItems that have child menu items the 
     * "current" level will "phase out" and the child menu items of the newly selected item will
     * be displayed. It's extremely versatile for displaying MASSIVE menu hierarchies due to that
     * in addition to having static menu items it also have support for having dynamically rendered
     * menu items, just like the TreeView. The SliderMenu also features a Bread Crumb at the top which
     * the user can use to fast scroll backwards in the hierarchy to his original place.
     * A SliderMenu consists of SliderMenuLevel items. Which in turn consists of SliderMenuItem items
     * which in turn can have SliderMenuLevel items and so on.
     */
    [ASP.ToolboxData("<{0}:SlidingMenu runat=\"server\"></{0}:SlidingMenu>")]
    public class SlidingMenu : Panel, ASP.INamingContainer
    {
        private Panel _breadParent = new Panel();
        private Panel _bread = new Panel();

        /**
         * Raised when a SliderMenuItem is selected by the user
         */
        public event EventHandler ItemClicked;

        /**
         * Overridden to provide a sane default value. The default CSS class of the SliderMenu is
         * "slider-menu".
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

        /**
         * Number of milliseconds that the MenuItems will spend animating when changing level.
         * The default value is 500 which is 0.5 seconds. The higher this number is, the more
         * time the menu items will spend "animating" into view when changing levels.
         */
        [DefaultValue(500)]
        public int AnimationDuration
        {
            get { return ViewState["AnimationDuration"] == null ? 500 : (int)ViewState["AnimationDuration"]; }
            set { ViewState["AnimationDuration"] = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // We MUST call the EnsureChildControls AFTER the ControlState has been loaded
            // since we're depending on some value from ControlState in order to correctly
            // instantiate the composition controls
            EnsureChildControls();

            // Defaulting all SliderMenuLevels to non-visible
            if (ActiveLevel == null)
            {
                foreach (ASP.Control idx in Controls)
                {
                    if (idx is SlidingMenuLevel)
                    {
                        SetAllChildrenNonVisible(idx);
                    }
                }
            }
        }

        protected override void CreateChildControls()
        {
            CreateBreadCrumbWrapper();
        }

        internal void EnsureBreadCrumbCreated()
        {
            if (ActiveLevel != null)
            {
                UpdateBreadCrumb(AjaxManager.Instance.FindControl<SlidingMenuLevel>(ActiveLevel));
            }
            else
            {
                UpdateBreadCrumb(null);
            }
        }

        private void CreateBreadCrumbWrapper()
        {
            _breadParent.ID = "breadParent";
            _breadParent.CssClass = "bread-crumb-parent";

            _bread.ID = "bread";
            _bread.CssClass = "bread-crumb";

            // To make sure it shows even when no path...
            WEBCTRLS.Literal lit = new WEBCTRLS.Literal();
            lit.ID = "litDummy";
            lit.Text = "&nbsp;";
            _bread.Controls.Add(lit);

            _breadParent.Controls.Add(_bread);

            Controls.AddAt(0, _breadParent);
        }

        internal void SetAllChildrenNonVisible(ASP.Control from)
        {
            foreach (ASP.Control idx in from.Controls)
            {
                if (idx is SlidingMenuLevel)
                {
                    (idx as SlidingMenuLevel).Style["display"] = "none";
                }
                SetAllChildrenNonVisible(idx);
            }
        }

        internal void RaiseItemClicked(SlidingMenuItem item)
        {
            if (ItemClicked != null)
                ItemClicked(item, new EventArgs());
        }

        internal string ActiveLevel
        {
            get { return ViewState["ActiveLevel"] as string; }
            set { ViewState["ActiveLevel"] = value; }
        }

        internal Panel BreadCrumb
        {
            get { return _bread; }
        }

        internal void SetActiveLevel(SlidingMenuLevel level)
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

        private void UpdateBreadCrumb(SlidingMenuLevel to)
        {
            ASP.Control idx = to;
            bool first = true;
            while (true)
            {
                if (idx == null || idx is SlidingMenu)
                {
                    break; // Finished
                }
                else if (idx is SlidingMenuItem)
                {
                    RaWebControl btn = null;
                    if (first)
                    {
                        btn = new Label();
                        btn.CssClass = "bread-item-left bread-item-last";
                        first = false;
                    }
                    else
                    {
                        btn = new LinkButton();
                        btn.CssClass = "bread-item-left";
                        btn.Click += btn_Click;
                    }
                    SlidingMenuLevel curLevel = null;
                    foreach (ASP.Control idx2 in idx.Controls)
                    {
                        if (idx2 is SlidingMenuLevel)
                        {
                            btn.ID = "BTN" + idx2.ID;
                            curLevel = idx2 as SlidingMenuLevel;
                        }
                    }

                    Label right = new Label();
                    right.ID = btn.ID + "r";
                    right.CssClass = "bread-item-right";
                    btn.Controls.Add(right);

                    Label center = new Label();
                    center.ID = btn.ID + "c";
                    center.CssClass = "bread-item-center";
                    center.Text =
                        string.IsNullOrEmpty(curLevel.Caption) ? 
                            (idx as SlidingMenuItem).Text :
                            curLevel.Caption;
                    right.Controls.Add(center);

                    _bread.Controls.AddAt(0, btn);
                }
                idx = idx.Parent;
            }
            // Creating home bread-crumb button
            RaWebControl home = null;
            if (to == null)
            {
                home = new Label();
            }
            else
            {
                home = new LinkButton();
                home.Click += btn_Click;
            }
            home.ID = "BTNbreadGoHome";
            home.CssClass = "bread-item-left first";

            Label right2 = new Label();
            right2.ID = home.ID + "r";
            right2.CssClass = "bread-item-right";
            home.Controls.Add(right2);

            Label center2 = new Label();
            center2.ID = home.ID + "c";
            center2.CssClass = "bread-item-center";
            center2.Text = "&nbsp;";
            right2.Controls.Add(center2);

            Label centerContent = new Label();
            centerContent.ID = home.ID + "cc";
            centerContent.CssClass = "slider-bread-home";
            centerContent.Text = "&nbsp;";
            center2.Controls.Add(centerContent);

            _bread.Controls.AddAt(0, home);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            string idOfToBecomeActive = btn.ID.Substring(3);
            SlidingMenuLevel previousActive = AjaxManager.Instance.FindControl<SlidingMenuLevel>(ActiveLevel);
            SlidingMenuLevel level = AjaxManager.Instance.FindControl<SlidingMenuLevel>(idOfToBecomeActive);
            int noLevels = 0;
            ASP.Control idxLevel = previousActive.Parent;
            while (true)
            {
                if (idxLevel is SlidingMenuLevel)
                {
                    noLevels += 1;
                }
                if (idxLevel == level)
                    break;
                if (idxLevel is SlidingMenuLevel)
                {
                    (idxLevel as SlidingMenuLevel).SetForReRendering();
                }
                idxLevel = idxLevel.Parent;
            }
            SetActiveLevel(level);

            // Animating Menu levels...
            ASP.Control rootLevel = null;
            foreach (ASP.Control idx in Controls)
            {
                if (idx is SlidingMenuLevel)
                {
                    rootLevel = idx;
                    break;
                }
            }
            _bread.Style["display"] = "gokk"; // To force a new value to the display property...
            _bread.Style["display"] = "none";
            new Ra.Extensions.SlidingMenuItem.EffectRollOut(rootLevel, _bread, AnimationDuration, true, noLevels)
                .Render();
        }
    }
}
