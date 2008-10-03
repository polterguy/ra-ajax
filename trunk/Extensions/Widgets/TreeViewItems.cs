/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
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
     * TreeView control's child items. Supports both dynamically and 
     * statically (.ASPX markup) created TreeViewItems.
     */
    [ASP.ToolboxData("<{0}:TreeViewItem runat=\"server\"></{0}:TreeViewItem>")]
    public class TreeViewItem : RaWebControl, ASP.INamingContainer
    {
        // Since we're instantiating an effect which we cannot render before the controls
        // have been "re-arranged" we have it as a field on the class.
        private Effect _effect = null;

        // Composition controls
        private LinkButton _expander;
        private Label _childrenContainer;

        /**
         * Raised when item needs to fetch child TreeViewItems. Note that to save bandwidth space while
         * at the same time have support for events on dynamically created child controls like CheckBox
         * and RadioButton controls the runtime will raise this event every callback after the TreeView 
         * is expanded for the first time. This means that the event handler for this event should NOT 
         * spend a long time fetching items. If it does the entire Ajax runtime will become slow!
         */
        public event EventHandler GetChildItems;

        /**
         * If true then item is expanded and child items will show up.
         */
        [DefaultValue(false)]
        public bool Expanded
        {
            get { return ViewState["Expanded"] == null ? false : (bool)ViewState["Expanded"]; }
            set { ViewState["Expanded"] = value; }
        }

        /**
         * This is the container control which actually contains the child TreeViewItems of
         * the current treeviewitem
         */
        public Label ChildContainer
        {
            get { return _childrenContainer; }
        }

        public void AddTreeViewItem(TreeViewItem item)
        {
            _childrenContainer.Controls.Add(item);
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            CreateCompositionControls();
            GetDynamicItems();

            // Moving controls to where they SHOULD be...
            ReArrangeControls();
        }

        private void CreateCompositionControls()
        {
            // Creating link button that expands and closes the child item collection
            _expander = new LinkButton();
            _expander.Text = "+";
            _expander.ID = "expanderBtn";
            _expander.Click += _expander_Click;
            Controls.AddAt(0, _expander);

            // Creating children container
            _childrenContainer = new Label();
            _childrenContainer.Tag = "ul";
            _childrenContainer.Style["display"] = Expanded ? "" : "none";
            _childrenContainer.ID = "childCollection";
            Controls.Add(_childrenContainer);
        }

        private void GetDynamicItems()
        {
            if (Expanded && GetChildItems != null)
            {
                GetChildItems(this, new EventArgs());
                _childrenContainer.Visible = true;
            }
        }

        private void _expander_Click(object sender, EventArgs e)
        {
            if (!Expanded)
            {
                Expanded = !Expanded;
                GetDynamicItems();
                _effect = new EffectFadeIn(_childrenContainer, 200);
            }
            else
            {
                Expanded = !Expanded;
                _effect = new EffectFadeOut(_childrenContainer, 200);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            // Moving controls to where they SHOULD be...
            ReArrangeControls();

            // Checking to see what to render in expander LinkButton
            // TODO: Implement CSS class instead of stupid +/- sign...
            if (Expanded)
                _expander.Text = "-";
            else
                _expander.Text = "+";

            // Deferring rendering of effects till the control IDs are correct...
            if (_effect != null)
                _effect.Render();

            // Checking to see if we've got child items, if not we set _childContainer to IN-visible...
            bool hasChildren = false;
            foreach (ASP.Control idx in _childrenContainer.Controls)
            {
                if (idx is TreeViewItem)
                    hasChildren = true;
                break;
            }
            if (!hasChildren)
            {
                // Control does not have children, therefor we render the child container control 
                // initially in-visible and later make it visible if it gets children...
                _childrenContainer.Visible = false;
                if (GetChildItems == null)
                {
                    // Control does NOT have children and does NOT have an event handler
                    // for getting "dynamic" items. Therefor we can safely make the Expand
                    // LinkButton IN-visible...
                    _expander.Visible = false;
                }
            }

            // Calling base...
            base.OnPreRender(e);
        }

        private void ReArrangeControls()
        {
            // Moving all controls where they SHOULD be
            // This means keeping all controls where they are except for TreeViewItems
            // which are stuffed into the _childrenContainer control...
            List<ASP.Control> controls = new List<ASP.Control>();
            foreach (ASP.Control idx in Controls)
            {
                if (idx is TreeViewItem)
                    controls.Add(idx);
            }
            foreach (ASP.Control idx in controls)
            {
                Controls.Remove(idx);
                _childrenContainer.Controls.Add(idx);
            }
        }

        protected override string GetOpeningHTML()
        {
            return string.Format("<li id=\"{0}\"{1}{2}>",
                ClientID,
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute());
        }

        protected override string GetClosingHTML()
        {
            return "</li>";
        }

        // Must override this bugger to not break XHTML compliance on in-visible items...
        public override string GetInvisibleHTML()
        {
            return string.Format("<li id=\"{0}\" style=\"display:none;\" />");
        }
    }
}
