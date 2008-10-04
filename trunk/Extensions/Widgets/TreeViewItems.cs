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
    public class TreeViewItem : Panel, ASP.INamingContainer
    {
        public class GetChildItemsEventArgs : EventArgs
        {
            private TreeView _tree;

            internal GetChildItemsEventArgs(ASP.Control ctrl)
            {
                _tree = ctrl as TreeView;
            }

            public ASP.ControlCollection Children
            {
                get { return _tree.Controls; }
            }
        }

        // Since we're instantiating an effect which we cannot render before the controls
        // have been "re-arranged" we have it as a field on the class.
        private Effect _effect = null;

        // Composition controls
        private Label _icon;
        private Label[] _spacers;

        /**
         * Raised when item needs to fetch child TreeViewItems. Note that to save bandwidth space while
         * at the same time have support for events on dynamically created child controls like CheckBox
         * and RadioButton controls the runtime will raise this event every callback after the TreeView 
         * is expanded for the first time. This means that the event handler for this event should NOT 
         * spend a long time fetching items. If it does the entire Ajax runtime will become slow!
         */
        public event EventHandler<GetChildItemsEventArgs> GetChildItems;

        /**
         * Raised when item is selected
         */
        public event EventHandler Selected;

        /**
         * If true then item is expanded and child items will show up.
         */
        [DefaultValue(false)]
        public bool Expanded
        {
            get { return ViewState["Expanded"] == null ? false : (bool)ViewState["Expanded"]; }
            set { ViewState["Expanded"] = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            
            // For expanding child treeviewitem collection
            this.Click += new EventHandler(TreeViewItem_Click);

            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            CreateCompositionControls();
            //GetDynamicItems();
        }

        private void CreateCompositionControls()
        {
            // Spacers to give room form left border
            int numSpacers = 1;
            ASP.Control idx = this.Parent.Parent;
            while (idx is TreeView || idx is TreeViewItem)
            {
                numSpacers += 1;
                idx = idx.Parent.Parent;
            }
            _spacers = new Label[numSpacers];
            int idxNo;
            for (idxNo = 0; idxNo < numSpacers; idxNo++)
            {
                _spacers[idxNo] = new Label();
                _spacers[idxNo].ID = "spacer" + idxNo;
                string css = "spacer";
                TreeViewItem item = this;
                for (int idxItemNo = numSpacers - (idxNo + 1); idxItemNo > 0; idxItemNo--)
                {
                    item = item.Parent.Parent as TreeViewItem;
                }
                if (item == this)
                {
                    if (item.IsLeafNode)
                    {
                        if (item.HasChildren)
                        {
                            if (item.Expanded)
                            {
                                css += " lines linesMinus";
                            }
                            else
                                css += " lines linesPlus";
                        }
                        else
                            css += " lines linesEnd";
                    }
                    else
                    {
                        if (item.HasChildren)
                        {
                            if (item.Expanded)
                            {
                                css += " lines linesMinusCont";
                            }
                            else
                                css += " lines linesPlusCont";
                        }
                        else
                            css += " lines linesBreak";
                    }
                }
                else
                {
                    if (!item.IsLeafNode)
                        css += " lines linesOnly";
                }
                _spacers[idxNo].CssClass = css;
                Controls.AddAt(idxNo, _spacers[idxNo]);
            }

            // Icon wrapper
            _icon = new Label();
            _icon.ID = "iconControl";
            Controls.AddAt(idxNo, _icon);
        }

        private bool HasChildren
        {
            get
            {
                if (GetChildItems != null)
                    return true;
                foreach (ASP.Control idx in Controls)
                {
                    if (idx is TreeView)
                        return true;
                }
                return false;
            }
        }

        private bool IsLeafNode
        {
            get
            {
                bool retVal = false;
                foreach (ASP.Control idx in Parent.Controls)
                {
                    if (idx is TreeViewItem)
                        retVal = idx == this;
                }
                return retVal;
            }
        }

        private void GetDynamicItems()
        {
            if (Expanded && GetChildItems != null)
            {
                TreeView tree = null;
                foreach (ASP.Control idx in this.Controls)
                {
                    if (idx is TreeView)
                    {
                        tree = idx as TreeView;
                        break;
                    }
                }
                GetChildItems(this, new TreeViewItem.GetChildItemsEventArgs(tree));
            }
        }

        private void TreeViewItem_Click(object sender, EventArgs e)
        {
            ParentTree.SelectedItem = this;
            if (Selected != null)
                Selected(this, new EventArgs());
            if (!Expanded)
            {
                _spacers[_spacers.Length - 1].CssClass = _spacers[_spacers.Length - 1].CssClass.Replace("Plus", "Minus");

                // Just got expanded
                Expanded = !Expanded;
                GetDynamicItems();
                TreeView tree = null;
                foreach (ASP.Control idx in Controls)
                {
                    if (idx is TreeView)
                    {
                        tree = idx as TreeView;
                        break;
                    }
                }
                if (tree != null)
                {
                    _effect = new EffectRollDown(tree, 200);
                    _effect.Joined.Add(new EffectFadeIn());
                }
                else
                {
                    // "un"-expanding...
                    Expanded = !Expanded;
                }
            }
            else
            {
                _spacers[_spacers.Length - 1].CssClass = _spacers[_spacers.Length - 1].CssClass.Replace("Minus", "Plus");

                // Collapsed just now
                TreeView tree = null;
                foreach (ASP.Control idx in Controls)
                {
                    if (idx is TreeView)
                    {
                        tree = idx as TreeView;
                        break;
                    }
                }
                _effect = new EffectRollUp(tree, 200);
                _effect.Joined.Add(new EffectFadeOut());
                Expanded = !Expanded;
            }
        }

        private TreeView ParentTree
        {
            get
            {
                ASP.Control ctrl = this.Parent;
                while (ctrl != null && !(ctrl is TreeView))
                    ctrl = ctrl.Parent;
                return ctrl as TreeView;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            // Deferring rendering of effects till the control IDs are correct...
            // If _effect != null then we're rendering either the Collapse or the Expand effect...
            if (_effect != null)
                _effect.Render();

            BuildCss();

            // Checking to see if we've got child items, if not we set _childContainer to IN-visible...
            bool hasChildren = false;
            TreeView tree = null;
            foreach (ASP.Control idx in Controls)
            {
                if (idx is TreeView)
                {
                    tree = idx as TreeView;
                    hasChildren = true;
                    break;
                }
            }
            if (hasChildren)
            {
                tree.Style["display"] = Expanded ? "" : "none";
            }

            // Calling base...
            base.OnPreRender(e);
        }

        private void BuildCss()
        {
            string treeCssClass = ParentTree.CssClass;
            string tmpCssClass = treeCssClass + "-item";
            if (Expanded)
                tmpCssClass += " " + treeCssClass + "-item-expanded";
            else
                tmpCssClass += " " + treeCssClass + "-item-collapsed";
            if (ParentTree.SelectedItem == this)
                tmpCssClass += " selected";
            CssClass = tmpCssClass;

            _icon.CssClass = "icon" + " icon" + (Expanded ? "-expanded" : "-collapsed");
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
            return string.Format("<li id=\"{0}\" style=\"display:none;\" />", ClientID);
        }
    }
}
