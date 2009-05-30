/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
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
using Ra.Effects;

namespace Ra.Extensions.Widgets
{
    /**
     * Collection of TreeNode items within either a Tree or a TreeNode control. Every Tree and TreeNode
     * should have ONE and only ONE (or none) TreeNodes. This is the "glue" that makes it possible
     * to render both dynamically retrieved items and static items. Though for every TreeNodes control
     * you must choose either one or the other. Meaning if you want to have the TreeNode items dynamically
     * rendered you should add up an event handler for GetChildNodes but have ZERO statically rendered item
     * within it. And if you want the items to be statically rendered in the markup you should NOT create
     * a GetChildNodes event handler for it but only add up items statically inside of it.
     */
    [ASP.ToolboxData("<{0}:TreeNodes runat=\"server\"></{0}:TreeNodes>")]
    public class TreeNodes : Panel, ASP.INamingContainer
    {
        private bool _hasLoadedDynamicControls;
        private bool _expanded;

        /**
         * Raised when control needs to fetch TreeNode children. Note that to save bandwidth space while
         * at the same time have support for events on dynamically created child controls like CheckBox
         * and RadioButton controls the runtime will raise this event EVERY callback after the Tree or
         * TreeNode is expanded for the first time. This means that the event handler for this event 
         * should NOT spend a long time fetching items. If it does the entire Ajax runtime will become slow!
         */
        public event EventHandler GetChildNodes;

        /**
         * If true then item is expanded and child items will show up in the markup. Note that
         * if this node is not expanded it might still have items rendered in the DOM. But
         * they will not be visible for the user.
         */
        [DefaultValue(false)]
        public bool Expanded
        {
            get { return _expanded; }
            set { _expanded = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            Tag = "ul";
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // We MUST call the EnsureChildControls AFTER the ControlState has been loaded
            // since we're depending on some value from ControlState in order to correctly
            // instantiate the composition controls
            EnsureChildControls();
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!(this.Parent is TreeNode) && !(this.Parent is Tree))
                throw new Exception("TreeNodes can only be child of Tree and TreeNode controls");
            base.OnPreRender(e);
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            // Reloading dynamic items, but only if the have been loaded before...
            if (_hasLoadedDynamicControls)
                GetDynamicNodes();
        }

        protected override void LoadControlState(object savedState)
        {
            if (savedState != null)
            {
                object[] tmp = savedState as object[];
                // Mono sometimes messes up the types here for what reasons I don't know... :(
                // It appears that it tries to load control state for objects added dynamically this round
                // which obviously is WRONG...!
                if (tmp != null)
                {
                    if (tmp[0] != null && tmp[0].GetType() == typeof(bool))
                    {
                        _hasLoadedDynamicControls = (bool)tmp[0];
                    }
                    if (tmp[1] != null && tmp[1].GetType() == typeof(bool))
                    {
                        _expanded = (bool)tmp[1];
                    }
                    base.LoadControlState(tmp[2]);
                }
            }
        }

        protected override object SaveControlState()
        {
            object[] retVal = new object[3];
            retVal[0] = _hasLoadedDynamicControls;
            retVal[1] = _expanded;
            retVal[2] = base.SaveControlState();
            return retVal;
        }

        internal bool HasChildren
        {
            get
            {
                foreach (ASP.Control idx in Controls)
                {
                    if (idx is TreeNode)
                        return true;
                }
                return false;
            }
        }

        internal bool HasChildrenMaybe
        {
            get
            {
                if (GetChildNodes != null)
                    return true;
                return HasChildren;
            }
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
            {
                GetDynamicNodes();
            }
        }

        // Used for animating down when expanded
        public void RollDown()
        {
            Expanded = true;
            new EffectRollDown(this, 200)
                .JoinThese(new EffectFadeIn())
                .Render();
        }

        // Used for animating up when collapsed
        public void RollUp()
        {
            Expanded = false;
            new EffectRollUp(this, 200)
                .JoinThese(new EffectFadeOut())
                .Render();
        }
    }
}
