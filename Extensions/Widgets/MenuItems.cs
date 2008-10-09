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
    [ASP.ToolboxData("<{0}:MenuItems runat=\"server\"></{0}:MenuItems>")]
    public class MenuItems : RaWebControl, ASP.INamingContainer
    {
        private bool _hasLoadedDynamicControls;

        public event EventHandler GetChildNodes;

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
            // since we're depending on some value from ControlState in order to correctly
            // instantiate the composition controls
            EnsureChildControls();
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
        internal void RollDown()
        {
            new EffectRollDown(this, 200)
                .JoinThese(new EffectFadeIn())
                .Render();
        }

        // Used for animating up when collapsed
        internal void RollUp()
        {
            new EffectRollUp(this, 200)
                .JoinThese(new EffectFadeIn())
                .Render();
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
    }
}
