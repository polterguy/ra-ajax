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
     * Wrapper arouns TreeNodes which again is a wrapper around TreeNode, this is the "root" 
     * element of a TreeView. The basic logis is that you have one Tree root object in your 
     * code. Then you have one TreeNodes inside of there where you put your TreeNode objects.
     * Then for every TreeNode that have children you add up one TreeNodes and inside of
     * that one put your child TreeNode items.
     */
    [ASP.ToolboxData("<{0}:Tree runat=\"server\"></{0}:Tree>")]
    public class Tree : RaWebControl, ASP.INamingContainer
    {
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
            SingleClickPlusSign
        }

        /**
         * Raised when a TreeNode is selected by the user
         */
        public event EventHandler SelectedNodeChanged;

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
        public bool AllowMultipleSelectedItems
        {
            get
            {
                if (ViewState["AllowMultipleSelectedItems"] == null)
                    return false;
                return (bool)ViewState["AllowMultipleSelectedItems"];
            }
            set
            {
                ViewState["AllowMultipleSelectedItems"] = value;
            }
        }

        internal void RaiseSelectedNodeChanged()
        {
            if (SelectedNodeChanged != null)
                SelectedNodeChanged(this, new EventArgs());
        }

        protected override string GetOpeningHTML()
        {
            return string.Format("<div id=\"{0}\"{1}>",
                ClientID,
                GetWebControlAttributes());
        }

        protected override string GetClosingHTML()
        {
            return "</div>";
        }
    }
}