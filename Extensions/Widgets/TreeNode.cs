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
     * statically (.ASPX markup) created treenodes through the TreeNodes class.
     */
    [ASP.ToolboxData("<{0}:TreeNode runat=\"server\"></{0}:TreeNode>")]
    public class TreeNode : Panel, ASP.INamingContainer
    {
        // Composition controls
        private Label[] _spacers;
        private Label _expander;
        private Label _icon;

        protected override void OnInit(EventArgs e)
        {
            this.Click += new EventHandler(TreeNode_Click);
            EnsureChildControls();
            base.OnInit(e);
        }

        protected override void CreateChildControls()
        {
            CreateCompositionControls();
        }

        private void CreateCompositionControls()
        {
            // Note that since (for simplicity) we're adding ALL "composition" controls
            // at the zeroth index, we're creating them in "opposite" order of appearance
            // Since every "new" control added to the Controls collection will "push" the previous
            // ones onwards out...

            // Icon wrapper
            _icon = new Label();
            _icon.ID = "iconControl";
            Controls.AddAt(0, _icon);

            // Expander wrapper
            _expander = new Label();
            _expander.ID = "expanderControl";
            Controls.AddAt(0, _expander);

            // Finding out how many spacers we need...
            int numSpacers = 0;
            ASP.Control idxParent = this.Parent.Parent;
            while (idxParent is TreeNode)
            {
                numSpacers += 1;
                idxParent = idxParent.Parent.Parent;
            }

            // Creating our spacer elements...
            _spacers = new Label[numSpacers];

            // Looping through and instantiating our spacers...
            for (int idxNo = 0; idxNo < numSpacers; idxNo++)
            {
                _spacers[idxNo] = new Label();
                _spacers[idxNo].ID = "spacer" + idxNo;
                Controls.AddAt(0, _spacers[idxNo]);
            }
        }

        private bool HasChildren
        {
            get
            {
                foreach (ASP.Control idx in Controls)
                {
                    if (idx is TreeNodes)
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

        private TreeNodes ChildTreeNodes
        {
            get
            {
                foreach (ASP.Control idx in Controls)
                {
                    if (idx is TreeNodes)
                        return idx as TreeNodes;
                }
                return null;
            }
        }

        private void TreeNode_Click(object sender, EventArgs e)
        {
            // Setting SelectedNode and raising the SelectedNodeChanged on the
            // parent Tree control
            ParentTree.SelectedNode = this;
            ParentTree.RaiseSelectedNodeChanged();

            // Expanding/Collapsing the ChildTreeNodes control
            if (ChildTreeNodes != null)
            {
                if (!ChildTreeNodes.Expanded)
                {
                    ChildTreeNodes.Expanded = true;
                    ChildTreeNodes.RollDown();
                    ChildTreeNodes.RaiseGetChildNodes();
                    _expander.CssClass = _expander.CssClass.Replace("Plus", "Minus");
                }
                else
                {
                    ChildTreeNodes.Expanded = false;
                    ChildTreeNodes.RollUp();
                    _expander.CssClass = _expander.CssClass.Replace("Minus", "Plus");
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            BuildCss();
            base.OnPreRender(e);
        }

        private void BuildCss()
        {
            BuildCssForRootElement();
            BuildCssForIcon();
            BuildCssForExpander();
            BuildCssForSpacers();
            SetPropertiesForChildren();
        }

        private void SetPropertiesForChildren()
        {
            if (ChildTreeNodes != null)
            {
                bool hasChildren = false;
                foreach (ASP.Control idx in ChildTreeNodes.Controls)
                {
                    if (idx is TreeNode)
                    {
                        hasChildren = true;
                        break;
                    }
                }
                if (!ChildTreeNodes.Expanded)
                {
                    ChildTreeNodes.Style["display"] = "none";
                }
                ChildTreeNodes.Visible = hasChildren;
            }
        }

        private void BuildCssForSpacers()
        {
            TreeNode idxNode = this.Parent.Parent as TreeNode;
            foreach (Label idx in _spacers)
            {
                idx.CssClass = "spacer" + (idxNode.IsLeafNode ? "" : " lines linesOnly");
                idxNode = idxNode.Parent.Parent as TreeNode;
            }
        }

        private void BuildCssForExpander()
        {
            if (ChildTreeNodes == null)
            {
                _expander.CssClass = IsLeafNode ? "spacer lines linesEnd" : "spacer lines linesBreak";
            }
            else
            {
                if (ChildTreeNodes.Expanded)
                {
                    _expander.CssClass = IsLeafNode ? "spacer lines linesMinusCont" : "spacer lines linesMinus";
                }
                else
                {
                    _expander.CssClass = IsLeafNode ? "spacer lines linesPlusCont" : "spacer lines linesPlus";
                }
            }
        }

        private void BuildCssForIcon()
        {
            _icon.CssClass = "icon" + " icon" + (ChildTreeNodes != null && ChildTreeNodes.Expanded ? "-expanded" : "-collapsed");
        }

        private void BuildCssForRootElement()
        {
            string treeCssClass = ParentTree.CssClass;
            string tmpCssClass = treeCssClass + "-item";

            if (ChildTreeNodes != null && ChildTreeNodes.Expanded)
                tmpCssClass += " " + treeCssClass + "-item-expanded";
            else
                tmpCssClass += " " + treeCssClass + "-item-collapsed";

            if (ParentTree.SelectedNode == this)
                tmpCssClass += " selected";

            CssClass = tmpCssClass;
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
