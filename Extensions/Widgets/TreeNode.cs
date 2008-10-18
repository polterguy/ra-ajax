/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
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
     * TreeNodes actual child items. This is the actual treenode for the TreeNodes control
     */
    [ASP.ToolboxData("<{0}:TreeNode runat=\"server\"></{0}:TreeNode>")]
    public class TreeNode : Panel, ASP.INamingContainer
    {
        // Composition controls
        private WEBCTRLS.Label[] _spacers;
        private Label _expander;
        private Label _icon;

        protected override void OnInit(EventArgs e)
        {
            EnsureChildControls();
            if (ParentTree.Expansion == Tree.ExpansionType.SingleClickEntireRow)
            {
                this.Click += TreeNode_Click;
                this.Click += ExpansionWidget_Click;
            }
            else if (ParentTree.Expansion == Tree.ExpansionType.SingleClickPlusSign)
            {
                this.Click += TreeNode_Click;
                _expander.Click += ExpansionWidget_Click;
            }
            base.OnInit(e);
        }

        public RaWebControl ExpanderControl
        {
            get { return _expander; }
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
            // NOTE!
            // This means that ORDER COUNTS here...!!

            // Icon wrapper
            _icon = new Label();
            _icon.ID = "iconControl";
            Controls.AddAt(0, _icon);

            // Expander wrapper
            _expander = new Label();
            _expander.ID = "expanderControl";
            Controls.AddAt(0, _expander);

            // Then creating our "spacer" controls
            CreateSpacers();
        }

        private void CreateSpacers()
        {
            // Finding out how many spacers we need...
            int numSpacers = 0;
            ASP.Control idxParent = this.Parent.Parent;
            while (idxParent is TreeNode)
            {
                numSpacers += 1;
                idxParent = idxParent.Parent.Parent;
            }

            // Creating our spacer elements...
            _spacers = new WEBCTRLS.Label[numSpacers];

            // Looping through and instantiating our spacers...
            for (int idxNo = 0; idxNo < numSpacers; idxNo++)
            {
                _spacers[idxNo] = new WEBCTRLS.Label();
                _spacers[idxNo].ID = "spacer" + idxNo;
                Controls.AddAt(0, _spacers[idxNo]);
            }
        }

        private bool IsLeafNode
        {
            get
            {
                // This logic will loop through all TreeNode items in the controls collections
                // and change the value of the "retVal" back and forth but the LAST time
                // it is changed it will be true ONLY if the last node is the "this" node...
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
                // Looping upwards in the Control hierarchy until we 
                // find a Control of type "Tree"
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

        private void ExpansionWidget_Click(object sender, EventArgs e)
        {
            // Expanding/Collapsing the ChildTreeNodes control
            if (ChildTreeNodes != null)
            {
                if (!ChildTreeNodes.Expanded)
                {
                    ChildTreeNodes.Expanded = true;
                    ChildTreeNodes.RollDown();
                    ChildTreeNodes.RaiseGetChildNodes();
                }
                else
                {
                    ChildTreeNodes.Expanded = false;
                    ChildTreeNodes.RollUp();
                }
            }
        }

        private void TreeNode_Click(object sender, EventArgs e)
        {
            // Setting SelectedNode and raising the SelectedNodeChanged on the
            // parent Tree control
            if( ParentTree.AllowMultipleSelectedItems)
            {
                List<TreeNode> nodes = new List<TreeNode>(ParentTree.SelectedNodes);
                
                if (!nodes.Remove(this))
                    nodes.Add(this);

                ParentTree.SelectedNodes = nodes.ToArray();
            }
            else
            {
                TreeNode[] node = new TreeNode[1];
                node[0] = this;
                ParentTree.SelectedNodes = node;
            }
            ParentTree.RaiseSelectedNodeChanged();
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

        // Build CSS classes for the "root" DOM element ("this control")
        private void BuildCssForRootElement()
        {
            string tmpCssClass = "";

            if (ChildTreeNodes == null)
                tmpCssClass += "no-childs";
            else if (ChildTreeNodes.Expanded)
                tmpCssClass += "expanded";
            else
                tmpCssClass += "collapsed";

            if (Array.Exists(ParentTree.SelectedNodes, delegate(TreeNode node) { return node == this; }))
                tmpCssClass += " selected";

            tmpCssClass += " item ";

            if (!string.IsNullOrEmpty(CssClass))
            {
                bool hasPreviouslySetTreeStuff = CssClass.IndexOf(" item ") != -1;
                if (hasPreviouslySetTreeStuff)
                {
                    string oldCssClass = CssClass.Substring(CssClass.IndexOf(" item ") + 6);
                    tmpCssClass += " " + oldCssClass;
                }
                else
                {
                    tmpCssClass += " " + CssClass;
                }
            }

            CssClass = tmpCssClass;
        }

        // Builds the CSS classes for the icon to the left of the "content" of the control
        private void BuildCssForIcon()
        {
            _icon.CssClass = "icon spacer";
        }

        // Builds the CSS classes for the expander plus/minus sign
        private void BuildCssForExpander()
        {
            if (ChildTreeNodes == null || !ChildTreeNodes.HasChildrenMaybe)
            {
                _expander.CssClass = IsLeafNode ? "icon leaf spacer" : "icon no-leaf spacer";
            }
            else
            {
                _expander.CssClass = IsLeafNode ? "icon leaf expander spacer" : "icon no-leaf expander spacer";
            }
        }

        // Builds CSS classes for the spacer labels.
        private void BuildCssForSpacers()
        {
            TreeNode idxNode = this.Parent.Parent as TreeNode;
            foreach (WEBCTRLS.Label idx in _spacers)
            {
                idx.CssClass = "spacer" + (idxNode.IsLeafNode ? "" : " lines");
                idxNode = idxNode.Parent.Parent as TreeNode;
            }
        }

        private void SetPropertiesForChildren()
        {
            if (ChildTreeNodes != null)
            {
                // Note that we're checking to see if the TreeNodes child
                // of this one actually HAVE children in the Controls Collection.
                // This is CRUCIAL since if it does NOT have children the TreeNodes should
                // NOT be rendered visible since then it will first of all pollute markup
                // and second of all NOT render correctly when children are being retrieved
                // and control should be made visible (since we're not doing ReRender on it
                // when it is expanded after retrieving items)
                // Note also that technically this should have been implemented in the TreeNodes
                // class, but unfortunately the OnPreRender will NOT be called if the 
                // control is not visible...
                // Therefor we have this logic here...
                ChildTreeNodes.Visible = ChildTreeNodes.HasChildren;

                // If the control is not Expanded we render it as display:none;
                if (!ChildTreeNodes.Expanded)
                {
                    ChildTreeNodes.Style["display"] = "none";
                }
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
            return string.Format("<li id=\"{0}\" style=\"display:none;\" />", ClientID);
        }
    }
}
