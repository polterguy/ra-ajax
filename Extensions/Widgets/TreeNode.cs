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
     * Tree control's child items. Supports both dynamically and 
     * statically (.ASPX markup) created TreeViewItems.
     */
    [ASP.ToolboxData("<{0}:TreeNode runat=\"server\"></{0}:TreeNode>")]
    public class TreeNode : Panel, ASP.INamingContainer
    {
        // Since we're instantiating an effect which we cannot render before the controls
        // have been "re-arranged" we have it as a field on the class.
        private Effect _effect = null;

        // Composition controls
        private Label _icon;
        private Label[] _spacers;

        /**
         * Raised when item needs to fetch child TreeViewItems. Note that to save bandwidth space while
         * at the same time have support for events on dynamically created child controls like CheckBox
         * and RadioButton controls the runtime will raise this event every callback after the Tree 
         * is expanded for the first time. This means that the event handler for this event should NOT 
         * spend a long time fetching items. If it does the entire Ajax runtime will become slow!
         */
        public event EventHandler GetChildItems;

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
            GetDynamicItems();
        }

        private void CreateCompositionControls()
        {
            // Spacers to give room form left border
            int numSpacers = 1;
            ASP.Control idx = this.Parent.Parent;
            while (idx is Tree || idx is TreeNode)
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
                TreeNode item = this;
                for (int idxItemNo = numSpacers - (idxNo + 1); idxItemNo > 0; idxItemNo--)
                {
                    item = item.Parent.Parent as TreeNode;
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
                    if (idx is Tree)
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
                    if (idx is TreeNode)
                        retVal = idx == this;
                }
                return retVal;
            }
        }

        private void GetDynamicItems()
        {
            if (Expanded && GetChildItems != null)
            {
                Tree tree = null;
                foreach (ASP.Control idx in this.Controls)
                {
                    if (idx is Tree)
                    {
                        tree = idx as Tree;
                        break;
                    }
                }
                GetChildItems(this, new EventArgs());
                tree.Visible = tree.Controls.Count > 0;
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
                Tree tree = null;
                foreach (ASP.Control idx in Controls)
                {
                    if (idx is Tree)
                    {
                        tree = idx as Tree;
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
                Tree tree = null;
                foreach (ASP.Control idx in Controls)
                {
                    if (idx is Tree)
                    {
                        tree = idx as Tree;
                        break;
                    }
                }
                _effect = new EffectRollUp(tree, 200);
                _effect.Joined.Add(new EffectFadeOut());
                Expanded = !Expanded;
            }
        }

        private Tree ParentTree
        {
            get
            {
                ASP.Control ctrl = this.Parent;
                while (ctrl != null && !(ctrl is Tree))
                    ctrl = ctrl.Parent;
                return ctrl as Tree;
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
            Tree tree = null;
            foreach (ASP.Control idx in Controls)
            {
                if (idx is Tree)
                {
                    tree = idx as Tree;
                    hasChildren = tree.Controls.Count > 0;
                    break;
                }
            }
            if (hasChildren)
            {
                tree.Style["display"] = Expanded ? "" : "none";
            }
            else
            {
                if (tree != null)
                    tree.Visible = false;
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
