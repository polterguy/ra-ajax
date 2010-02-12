/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
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

namespace Ra.Extensions.Widgets
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
        private readonly Panel _breadParent = new Panel();
        private readonly Panel _bread = new Panel();

        /**
         * Raised when a SliderMenuItem is selected by the user
         */
        public event EventHandler ItemClicked;

        /**
         * Raised when user navigates within the SlidingMenu by either going to a sub-level
         * or by clicking the bread-crumb items to go backwards.
         */
        public event EventHandler Navigate;

        /**
         * Overridden to provide a sane default value. The default CSS class of the SliderMenu is
         * "slider-menu".
         */
        [DefaultValue("sliding-menu")]
        public override string CssClass
        {
            get
            {
                string retVal = base.CssClass;
                if (retVal == string.Empty)
                    retVal = "sliding-menu";
                return retVal;
            }
            set { base.CssClass = value; }
        }

        /**
         * If you set this property then the Control you set here will be used instead of the
         * bread-crumb wrapped within the actual SlidingMenu. This is useful for scenarios
         * where you don't want the bread-crumb to be a part of the actual SlidingMenu but
         * rather on the "outside" so that e.g the bread-crumb wraps your entire page istead
         * of being constrained to the width of the SlidingMenu itself. Notice also 
         * though that if you set this value then the Control you choose as the BreadCrumbControl 
         * MUST have a Parent HTML element (doesn't need to be a server control) and that
         * HTML element MUST have an id property. It must also have an explicit width,
         * overflow hidden and a position of relative. The BreadCrumb control you choose
         * as the bread crumb should also probably have the CssClass property of 
         * "bread-crumb-parent" unless you want to build your own CSS which might be quite 
         * difficult unless you really know what you're doing. A good example of this would
         * normally be something like this;
         * <pre>
         * &lt;div 
         *    style="width:400px;overflow:hidden;position:relative;" 
         *    id="something"&gt;
         *    &lt;ra:Panel 
         *       runat="server" 
         *       ID="customBreadParent" 
         *       CssClass="bread-crumb-parent" /&gt;
         * &lt;/div&gt;
         * </pre>
         */
        public string CustomBreadCrumbControl
        {
            get { return ViewState["CustomBreadCrumbControl"] as string; }
            set { ViewState["CustomBreadCrumbControl"] = value; }
        }

        internal RaWebControl ActualBreadCrumbParent
        {
            get
            {
                if (!string.IsNullOrEmpty(CustomBreadCrumbControl))
                {
                    return AjaxManager.Instance.FindControl<RaWebControl>(CustomBreadCrumbControl);
                }
                return _breadParent;
            }
        }

        internal RaWebControl BreadCrumb
        {
            get { return _bread; }
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

        private void CreateBreadCrumbWrapper()
        {
            _breadParent.ID = "breadParent";
            _breadParent.CssClass = "bread-crumb-parent";
            Controls.AddAt(0, _breadParent);

            _bread.ID = "bread";
            _bread.CssClass = "bread-crumb";
            ActualBreadCrumbParent.Controls.Add(_bread);
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

        internal void SetAllChildrenNonVisible(ASP.Control from)
        {
            foreach (ASP.Control idx in from.Controls)
            {
                SlidingMenuLevel lev = idx as SlidingMenuLevel;
                if (lev != null)
                {
                    if (lev.Style[Styles.display] != "none")
                        lev.Style["display"] = "none";
                }
                SetAllChildrenNonVisible(idx);
            }
        }

        internal void RaiseItemClicked(SlidingMenuItem item)
        {
            if (ItemClicked != null)
                ItemClicked(item, new EventArgs());

            if (Navigate != null)
                Navigate(this, new EventArgs());
        }

        /**
         * The ActiveLevel of the SlidingMenu. This is the ID of the SlidingMenuLevel which
         * is currently showing in the pane. Or null if the root level is showing.
         */
        public string ActiveLevel
        {
            get { return ViewState["ActiveLevel"] as string; }
            internal set { ViewState["ActiveLevel"] = value; }
        }

        internal void SetActiveLevel(SlidingMenuLevel level)
        {
            ActiveLevel = level == null ? null : level.ID;
            _bread.Controls.Clear();
            UpdateBreadCrumb(level);
            _bread.ReRender();
        }

        /**
         * Will expand menu programatically to given path. The path should contain all ID's of
         * elements starting from first SlidingMenuItem. Note the ID's should be only the ID's to
         * the SlidingMenuItem's and not the Levels. The seperator between the ID's are slash (/).
         */
        public void SlideTo(string path)
        {
            List<string> ids = new List<string>(path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
            SlidingMenuItem item = Selector.Selector.FindControl<SlidingMenuItem>(this, ids[0]);
            ids.RemoveAt(0);
            OpenMenuItem(item, ids, ids.Count + 1);
        }

        private void OpenMenuItem(SlidingMenuItem item, List<string> ids, int totalLevels)
        {
            // Making sure item has loaded its children
            SlidingMenuLevel childLevel = item.FindChildLevel();
            if (childLevel == null)
            {
                // No more items underneath...
                childLevel = item.Parent as SlidingMenuLevel;
                totalLevels -= (ids.Count + 1);
                ids.Clear();
            }
            else
            {
                if (childLevel.EnsureChildNodes())
                    childLevel.ReRender();
            }

            // Checking to see if we're finished
            if (ids.Count == 0)
            {
                // Wrapping up...
                SetAllChildrenNonVisible(this);
                ASP.Control idxFromThis = childLevel;
                while (idxFromThis != null && !(idxFromThis is SlidingMenu))
                {
                    if (idxFromThis is SlidingMenuLevel)
                    {
                        (idxFromThis as SlidingMenuLevel).Style["display"] = "";
                    }
                    idxFromThis = idxFromThis.Parent;
                }
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
                BreadCrumb.Style["display"] = "gokk"; // To force a new value to the display property...
                BreadCrumb.Style["display"] = "none";
                if (ActiveLevel == null)
                {
                    new SlidingMenuItem.EffectRollOut(rootLevel,
                        BreadCrumb,
                        AnimationDuration,
                        false,
                        totalLevels)
                        .Render();
                }
                else
                {
                    new SlidingMenuItem.EffectRollOut(rootLevel,
                        BreadCrumb,
                        AnimationDuration,
                        true,
                        -1,
                        true)
                        .ChainThese(
                            new SlidingMenuItem.EffectRollOut(rootLevel,
                                BreadCrumb,
                                AnimationDuration,
                                false,
                                totalLevels))
                        .Render();
                }
                childLevel.Style["display"] = "";
                SetActiveLevel(childLevel);
            }
            else
            {
                SlidingMenuItem itemNext = Selector.Selector.FindControl<SlidingMenuItem>(childLevel, ids[0]);
                ids.RemoveAt(0);
                OpenMenuItem(itemNext, ids, totalLevels);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            // If a custom bread crumb is given, we make the default system given
            // one invisible...
            if (CustomBreadCrumbControl != null)
                _breadParent.Visible = false;

            base.OnPreRender(e);
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
                            btn.ID = "rxB" + idx2.ID;
                            curLevel = idx2 as SlidingMenuLevel;
                        }
                    }

                    Label right = new Label();
                    right.ID = btn.ID + "rxq";
                    right.CssClass = "bread-item-right";
                    btn.Controls.Add(right);

                    Label center = new Label();
                    center.ID = btn.ID + "cxq";
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
                home.Click += btnHome_Click;
            }
            home.ID = "BTNbreadGoHome";
            home.CssClass = "bread-item-left bread-item-first";

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
            centerContent.CssClass = "sliding-bread-home";
            centerContent.Text = "&nbsp;";
            center2.Controls.Add(centerContent);

            _bread.Controls.AddAt(0, home);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            SlideBackwards(btn, false);

            if (Navigate != null)
                Navigate(this, new EventArgs());
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            SlideBackwards(btn, true);

            if (Navigate != null)
                Navigate(this, new EventArgs());
        }

        private void SlideBackwards(LinkButton sender, bool allTheWay)
        {
            string idOfToBecomeActive = sender.ID.Substring(3);
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
                if (!(idx is SlidingMenuLevel))
                    continue;
                rootLevel = idx;
                break;
            }
            _bread.Style["display"] = "gokk"; // To force a new value to the display property...
            _bread.Style["display"] = "none";
            new SlidingMenuItem.EffectRollOut(
                rootLevel, 
                _bread, 
                AnimationDuration, 
                true, 
                noLevels,
                allTheWay)
                .Render();
        }
    }
}
