/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using ASP = System.Web.UI;
using WEBCTRLS = System.Web.UI.WebControls;

namespace Ra.Widgets
{
    [ASP.ToolboxData("<{0}:DynamicControl2 runat=\"server\" />")]
    public class DynamicControl2 : Panel
    {
        public delegate void DynamicLoadDelegate();

        public DynamicLoadDelegate DynamicLoad;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DynamicLoad != null && ViewState["_dynamicControlID"] != null)
                DynamicLoad();
        }

        public void AddControl(ASP.Control control)
        {
            if (control == null)
                return;

            if (Controls.Contains(control))
                Controls.Remove(control);

            ViewState["_dynamicControlID"] = control.ID;

            Controls.Add(control);
            ReRender();
        }

        public void RemoveControl(ASP.Control control)
        {
            ViewState["_dynamicControlID"] = null;

            if (control == null)
                return;
            if (Controls.Contains(control))
                Controls.Remove(control);
        }

        public void Show()
        {
            if (DynamicLoad != null)
                DynamicLoad();
        }
    }
}
