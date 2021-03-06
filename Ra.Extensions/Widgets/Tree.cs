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
     * Wrapper around TreeNodes which again is a wrapper around TreeNode. This is the "root" 
     * element of a TreeView. The basic logis is that you have one Tree root object in your 
     * code. Then you have one TreeNodes inside of there where you put your TreeNode objects.
     * Then for every TreeNode that have children you add up one TreeNodes and inside of
     * that one put your child TreeNode items.
     */
    [ASP.ToolboxData("<{0}:Tree runat=\"server\"></{0}:Tree>")]
    public class Tree : Panel, ASP.INamingContainer
    {
        /**
         * How and if items can be selected in the TreeView control
         */
        public enum SelectionModeType
        {
            /**
             * Only one TreeNode can be selected at the time
             */
            SingleSelection,

            /**
             * Multiple items can be selected in the TreeView
             */
            MultipleSelection,

            /**
             * Items can only be selected from code. Nothing the user does in regards 
             * to the TreeView can select nodes in the TreeView.
             */
            NoSelection
        }

        /**
         * Enum describing how TreeNode items in the Tree should be expanded
         */
        public enum ExpansionType
        {
            /**
             * Single clicking any place in row
             */
            SingleClickEntireRow,

            /**
             * Only plus sign can be clicked to expand TreeNode
             */
            SingleClickPlusSign,

            /**
             * Signals that expansions of TreeNodes is NOT allowed...!
             */
            None
        }

        /**
         * Raised when a TreeNode is selected by the user
         */
        public event EventHandler SelectedNodeChanged;

        /**
         * Overridden to provide a sane default value
         */
        [DefaultValue("tree")]
        public override string CssClass
        {
            get
            {
                string retVal = base.CssClass;
                if (retVal == string.Empty)
                    retVal = "tree";
                return retVal;
            }
            set { base.CssClass = value; }
        }

        /**
         * If true then the expanding of nodes will happen purely on the client IF POSSIBLE and
         * not create an Ajax Request at all. 
         * This value must be set when control is created or shown for the first time
         * and cannot be changed after control is created.
         */
        [DefaultValue(false)]
        public bool ClientSideExpansion
        {
            get
            {
                if (ViewState["ClientSideExpansion"] == null)
                    return false;
                return (bool)ViewState["ClientSideExpansion"];
            }
            set
            {
                ViewState["ClientSideExpansion"] = value;
            }
        }

        /**
         * If true then the Tree will use the Tree.js file which will give you some
         * additional benefits in regards to animation and such. The default value
         * is false, but if you set ClientSideExpansion to true, the Tree.js file will
         * anyway be included. The reason for this logic is so that you can logically
         * choose rich animation but also at the same time choose "whole row selection".
         */
        [DefaultValue(false)]
        public bool UseRichAnimations
        {
            get
            {
                if (ViewState["UseRichAnimations"] == null)
                    return false;
                return (bool)ViewState["UseRichAnimations"];
            }
            set
            {
                ViewState["UseRichAnimations"] = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (ClientSideExpansion || UseRichAnimations)
            {
                AjaxManager.Instance.IncludeScriptFromResource(typeof(TreeNode), "Ra.Extensions.Js.Tree.js");
            }

            int count = 0;
            foreach (ASP.Control idx in Controls)
            {
                if (idx is TreeNodes)
                    count += 1;
            }
            if (count != 1)
                throw new Exception("Every Tree Control must have EXACTLY *1* TreeNodes");
            base.OnPreRender(e);
        }

        /**
         * The nodes that have been currently selected by either the user or through code
         */
        public TreeNode[] SelectedNodes
        {
            get
            {
                if (ViewState["SelectedNodes"] == null)
                    return new TreeNode[0];

                string ids = (string)ViewState["SelectedNodes"];
                List<TreeNode> nodes = new List<TreeNode>();

                foreach (string id in ids.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    TreeNode node = AjaxManager.Instance.FindControl<TreeNode>(id);

                    nodes.Add(node);
                }
                
                return nodes.ToArray();
            }
            set
            {
                string nodes = string.Empty;

                foreach (TreeNode node in value)
                    nodes += node.ID + ",";

                ViewState["SelectedNodes"] = nodes;
            }
        }

        /**
         * How TreeNode items in the Tree should be expanded
         */
        [DefaultValue(ExpansionType.SingleClickEntireRow)]
        public ExpansionType Expansion
        {
            get
            {
                if (ViewState["Expansion"] == null)
                    return ExpansionType.SingleClickEntireRow;
                return (ExpansionType)ViewState["Expansion"];
            }
            set
            {
                ViewState["Expansion"] = value;
            }
        }

        /**
         * If true you can select multiple nodes by clicking CTRL while selecting
         */
        [DefaultValue(false)]
        [Obsolete("Instead of using this property you should use the SelectionMode property", true)]
        public bool AllowMultipleSelectedItems
        {
            get { throw new ApplicationException("Usage of obsolete property AllowMultipleSelectedItems, use SelectionMode instead"); }
            set { throw new ApplicationException("Usage of obsolete property AllowMultipleSelectedItems, use SelectionMode instead"); }
        }

        /**
         * Defines how/if the user can select items and if the user can select multiple items
         * at the same time.
         */
        [DefaultValue(SelectionModeType.SingleSelection)]
        public SelectionModeType SelectionMode
        {
            get
            {
                if (ViewState["SelectionMode"] == null)
                    return SelectionModeType.SingleSelection;
                return (SelectionModeType)ViewState["SelectionMode"];
            }
            set
            {
                ViewState["SelectionMode"] = value;
            }
        }

        protected override string GetClientSideScriptType()
        {
            if (ClientSideExpansion || UseRichAnimations)
            {
                return "new Ra.Tree";
            }
            return base.GetClientSideScriptType();
        }

        internal void RaiseSelectedNodeChanged()
        {
            if (SelectedNodeChanged != null)
                SelectedNodeChanged(this, new EventArgs());
        }
    }
}
