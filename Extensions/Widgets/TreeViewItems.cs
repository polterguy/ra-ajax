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
    [ASP.ToolboxData("<{0}:TreeViewItem runat=\"server\"></{0}:TreeViewItem>")]
    public class TreeViewItem : RaWebControl, ASP.INamingContainer
    {
        private LinkButton _expander;
        private Label _childrenContainer;

        public event EventHandler GetChildItems;

        [DefaultValue(false)]
        public bool Expanded
        {
            get { return ViewState["Expanded"] == null ? false : (bool)ViewState["Expanded"]; }
            set
            {
                ViewState["Expanded"] = value;
            }
        }

        public Label ChildContainer
        {
            get { return _childrenContainer; }
        }

        [Browsable(false)]
        public IEnumerable<TreeViewItem> Items
        {
            get
            {
                foreach (ASP.Control idx in Controls)
                {
                    if (idx is TreeViewItem)
                        yield return idx as TreeViewItem;
                }
            }
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            EnsureChildControls();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected override void CreateChildControls()
        {
            CreateCompositionControls();
            GetDynamicItems();
        }

        private void CreateCompositionControls()
        {
            // Creating link button that expands and closes the child item collection
            _expander = new LinkButton();
            _expander.Text = "+";
            _expander.ID = "expanderBtn";
            _expander.Click += new EventHandler(_expander_Click);
            Controls.AddAt(0, _expander);

            // Creating children container
            _childrenContainer = new Label();
            _childrenContainer.Tag = "ul";
            _childrenContainer.Style["display"] = Expanded ? "" : "none";
            _childrenContainer.ID = "childCollection";
            Controls.AddAt(1, _childrenContainer);

            // Moving controls to where they SHOULD be...
            ReArrangeControls();
        }

        private void GetDynamicItems()
        {
            if (GetChildItems != null)
                GetChildItems(this, new EventArgs());
        }

        private Effect _effect = null;
        private void _expander_Click(object sender, EventArgs e)
        {
            if (!Expanded)
            {
                if (GetChildItems != null)
                    GetChildItems(this, new EventArgs());
                _effect = new EffectFadeIn(_childrenContainer, 200);
            }
            else
            {
                _effect = new EffectFadeOut(_childrenContainer, 200);
            }
            Expanded = !Expanded;
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
                _childrenContainer.Visible = false;

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
