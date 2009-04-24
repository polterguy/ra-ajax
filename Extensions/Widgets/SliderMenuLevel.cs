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

namespace Ra.Extensions
{
    /**
     */
    [ASP.ToolboxData("<{0}:SliderMenuLevel runat=\"server\"></{0}:SliderMenuLevel>")]
    public class SliderMenuLevel : Panel, ASP.INamingContainer
    {
        private bool _hasLoadedDynamicControls;

        /**
         */
        public event EventHandler GetChildNodes;

        /**
         * Overridden to provide a sane default value
         */
        [DefaultValue("level")]
        public override string CssClass
        {
            get
            {
                string retVal = base.CssClass;
                if (retVal == string.Empty)
                    retVal = "level";
                return retVal;
            }
            set { base.CssClass = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            Tag = "ul";
            base.OnInit(e);
            if (this.Parent is SliderMenu)
                CssClass += " top-level";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // We MUST call the EnsureChildControls AFTER the ControlState has been loaded
            // since we're depending on some value from ControlState in order to correctly
            // instantiate the composition controls
            EnsureChildControls();
        }

        private SliderMenu Root
        {
            get
            {
                ASP.Control idx = this.Parent;
                while (idx != null && !(idx is SliderMenu))
                    idx = idx.Parent;
                return idx as SliderMenu;
            }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            // Reloading dynamic items, but only if the have been loaded before...
            if (_hasLoadedDynamicControls)
                GetDynamicNodes();

            if (Root.ActiveLevel == this.ID)
                Root.EnsureBreadCrumbCreated();
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
                    base.LoadControlState(tmp[1]);
                }
            }
        }

        protected override object SaveControlState()
        {
            object[] retVal = new object[2];
            retVal[0] = _hasLoadedDynamicControls;
            retVal[1] = base.SaveControlState();
            return retVal;
        }

        internal bool HasChildren
        {
            get
            {
                foreach (ASP.Control idx in Controls)
                {
                    if (idx is SliderMenuItem)
                        return true;
                }
                return false;
            }
        }

        internal bool EnsureChildNodes()
        {
            if (_hasLoadedDynamicControls)
                return false;
            foreach (ASP.Control idx in Controls)
            {
                if (idx is SliderMenuItem)
                    return false;
            }
            GetDynamicNodes();
            return true;
        }

        private void GetDynamicNodes()
        {
            if (GetChildNodes != null)
            {
                GetChildNodes(this, new EventArgs());
                _hasLoadedDynamicControls = true;
            }
        }
    }
}
