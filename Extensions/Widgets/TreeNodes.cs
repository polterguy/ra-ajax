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
     * Collection of TreeNode items within wither a TreeView or a TreeNode control
     */
    [ASP.ToolboxData("<{0}:TreeNodes runat=\"server\"></{0}:TreeNodes>")]
    public class TreeNodes : RaWebControl, ASP.INamingContainer
    {
        private bool _hasLoadedDynamicControls;

        /**
         * Raised when item needs to fetch child TreeViewItems. Note that to save bandwidth space while
         * at the same time have support for events on dynamically created child controls like CheckBox
         * and RadioButton controls the runtime will raise this event every callback after the Tree 
         * is expanded for the first time. This means that the event handler for this event should NOT 
         * spend a long time fetching items. If it does the entire Ajax runtime will become slow!
         */
        public event EventHandler GetChildNodes;

        /**
         * The Controls in the ControlCollection which are of type TreeNode
         */
        [Browsable(false)]
        public IEnumerable<TreeNode> Nodes
        {
            get
            {
                foreach (ASP.Control idx in Controls)
                {
                    if (idx is TreeNode)
                        yield return idx as TreeNode;
                }
            }
        }

        /**
         * If true then item is expanded and child items will show up.
         */
        [DefaultValue(false)]
        public bool Expanded
        {
            get { return ViewState["Expanded"] == null ? false : (bool)ViewState["Expanded"]; }
            set { ViewState["Expanded"] = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // We MUST call the EnsureChildControls AFTER the ControlState has been loaded
            // since we're depending on some value frmo ControlState in order to correctly
            // instantiate the composition controls
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            if (_hasLoadedDynamicControls)
                GetDynamicNodes();
        }

        protected override void LoadControlState(object savedState)
        {
            object[] tmp = savedState as object[];
            _hasLoadedDynamicControls = (bool)tmp[0];
            base.LoadControlState(tmp[1]);
        }

        protected override object SaveControlState()
        {
            object[] retVal = new object[2];
            retVal[0] = _hasLoadedDynamicControls;
            retVal[1] = base.SaveControlState();
            return retVal;
        }

        private void GetDynamicNodes()
        {
            if (GetChildNodes != null)
            {
                GetChildNodes(this, new EventArgs());
                _hasLoadedDynamicControls = true;
            }
        }

        internal void RaiseGetChildNodes()
        {
            if (!_hasLoadedDynamicControls)
                GetDynamicNodes();
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!Expanded)
                this.Style["display"] = "none";
            base.OnPreRender(e);
        }

        protected override string GetOpeningHTML()
        {
            return string.Format("<ul id=\"{0}\"{1}{2}>",
                ClientID,
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute());
        }

        protected override string GetClosingHTML()
        {
            return "</ul>";
        }

        internal void RollDown()
        {
            new EffectRollDown(this, 200)
                .JoinThese(new EffectFadeIn())
                .Render();
        }

        internal void RollUp()
        {
            new EffectRollUp(this, 200)
                .JoinThese(new EffectFadeIn())
                .Render();
        }
    }
}
