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
         * The node which have been currently selected by either the user or through code
         */
        public TreeNode SelectedNode
        {
            get
            {
                TreeNode item = null;
                if (ViewState["SelectedNode"] != null)
                {
                    item = AjaxManager.Instance.FindControl<TreeNode>(ViewState["SelectedNode"].ToString());
                }
                return item;
            }
            set
            {
                ViewState["SelectedNode"] = value.ID;
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
            return string.Format("<div id=\"{0}\"{1}{2}>",
                ClientID,
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute());
        }

        protected override string GetClosingHTML()
        {
            return "</div>";
        }
    }
}
